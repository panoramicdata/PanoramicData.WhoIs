using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Configuration;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Extensions;
using PanoramicData.WhoIs.Interfaces;
using PanoramicData.WhoIs.ProfileResult;
using PanoramicData.WhoIs.Test.Fakes;

namespace PanoramicData.WhoIs.Test;

public class EnhancerCompositionTests : TestBase
{
	private sealed class SuffixCompanyEnhancer : ICompanyEnhancer
	{
		public Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken)
			=> Task.FromResult(new Company { Name = company.Name + "!" });
	}

	private sealed class CompanyPersonEnhancer(IReadOnlyCollection<ICompanyEnhancer> companyEnhancers) : IPersonEnhancer
	{
		public IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers { get; } = companyEnhancers;

		public Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken) => Task.FromResult(person);
	}

	[Fact]
	public async Task Merge_PrefersSourceValuesAndUnionsCollections()
	{
		var source = new Person
		{
			FirstName = "Source",
			Company = new Company { Name = "SourceCo", Registrar = "R1" },
			Languages = ["en"],
			Awards = [new ProfileAwards { Title = "A" }],
		};
		var extra = new Person
		{
			FirstName = "Extra",
			LastName = "Last",
			Gender = "f",
			Age = 30,
			BirthYear = 1990,
			Country = "GB",
			City = "London",
			State = "England",
			Occupation = "Dev",
			InferredSalaryMin = "1",
			InferredSalaryMax = "2",
			Company = new Company { Name = "ExtraCo", RegistrarUrl = "url", RegistrantEmail = "e@x.example", CreationDate = new DateTime(2020, 1, 1) },
			Languages = ["en", "fr"],
			Courses = [new ProfileCourses { Name = "C" }],
		};

		var result = await new FakePersonEnhancer(extra).EnhanceAsync(source, CancellationToken);

		result.FirstName.Should().Be("Source");
		result.LastName.Should().Be("Last");
		result.Occupation.Should().Be("Dev");
		result.InferredSalaryMax.Should().Be("2");
		result.Company!.Name.Should().Be("SourceCo");
		result.Company.Registrar.Should().Be("R1");
		result.Company.RegistrarUrl.Should().Be("url");
		result.Company.RegistrantEmail.Should().Be("e@x.example");
		result.Company.CreationDate.Should().Be(new DateTime(2020, 1, 1));
		result.Languages.Should().BeEquivalentTo("en", "fr");
		result.Awards.Should().ContainSingle();
		result.Courses.Should().ContainSingle();
	}

	[Theory]
	[InlineData(true, true)]
	[InlineData(true, false)]
	[InlineData(false, true)]
	public async Task Merge_WithAMissingCompany_KeepsTheOtherOne(bool sourceHasCompany, bool extraHasCompany)
	{
		var source = new Person { Company = sourceHasCompany ? new Company { Name = "S" } : null };
		var extra = new Person { Company = extraHasCompany ? new Company { Name = "E" } : null };

		var result = await new FakePersonEnhancer(extra).EnhanceAsync(source, CancellationToken);

		result.Company!.Name.Should().Be(sourceHasCompany ? "S" : "E");
	}

	[Fact]
	public async Task PersonEnhancer_AppliesCompanyEnhancersOnlyWhenThereIsACompany()
	{
		var enhancer = new PersonEnhancer([new CompanyPersonEnhancer([new SuffixCompanyEnhancer(), new SuffixCompanyEnhancer()])]);
		using var _ = enhancer;

		var withCompany = await enhancer.EnhanceAsync(new Person { Company = new Company { Name = "Acme" } }, CancellationToken);
		var withoutCompany = await enhancer.EnhanceAsync(new Person(), CancellationToken);

		withCompany.Company!.Name.Should().Be("Acme!!");
		withoutCompany.Company.Should().BeNull();
	}

	[Fact]
	public void AddWhoIs_WithProxyCurl_RegistersTheEnhancers()
	{
		var services = new ServiceCollection();

		services.AddWhoIs(options => options.AddProxyCurl("cx", "g-key", "pc-key"));
		using var provider = services.BuildServiceProvider();

		provider.GetRequiredService<WhoIsOptions>().Should().NotBeNull();
		provider.GetServices<IPersonEnhancer>().Should().ContainSingle().Which.Should().BeOfType<ProxyCurlPersonEnhancer>();
		provider.GetRequiredService<PersonEnhancer>().Should().NotBeNull();
	}

	[Fact]
	public void EnumSearcher_EnumeratesAndResets()
	{
		var searcher = new EnumSearcher([FakeSearcher, AnotherFakeSearcher]);

		var beforeStart = () => searcher.Current;
		beforeStart.Should().Throw<InvalidOperationException>();

		searcher.MoveNext().Should().BeTrue();
		searcher.Current.Should().BeSameAs(FakeSearcher);
		searcher.MoveNext().Should().BeTrue();
		((System.Collections.IEnumerator)searcher).Current.Should().BeSameAs(AnotherFakeSearcher);
		searcher.MoveNext().Should().BeFalse();

		searcher.Reset();
		searcher.MoveNext().Should().BeTrue();
		searcher.Current.Should().BeSameAs(FakeSearcher);
	}
}