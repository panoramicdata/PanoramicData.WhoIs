using FluentAssertions;
using PanoramicData.WhoIs.Enhancers;
using System.Net.Mail;

namespace PanoramicData.WhoIs.Test;

public class PersonEnhancerTests : TestBase
{
	[Fact]
	public async Task PersonEnhancer_WithInvalidEmailAddress_ShouldThrowException()
	{
		var searcher = new PersonEnhancer([FakeSearcher]);

		var getResponse = async () =>
		{
			await searcher
			.EnhanceAsync(new Person { MailAddress = new MailAddress("asdfghjkl") }, default)
			.ConfigureAwait(false);
		};
		await getResponse.Should().ThrowAsync<FormatException>();
	}

	[Fact]
	public async Task DefaultPersonEnhancer_DoesAGoodJob()
	{
		var searcher = new DefaultPersonEnhancer();

		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = new MailAddress("john.smith@panoramicdata.com") }, default)
			.ConfigureAwait(false);

		response.Should().NotBeNull();
		response.FirstName.Should().Be("John");
		response.LastName.Should().Be("Smith");
		response.Company.Should().NotBeNull();
		response.Company.Name.Should().Be("Panoramic Data");
		response.Company.DomainName.Should().Be("panoramicdata.com");
		response.Company.Registrar.Should().NotBeNullOrWhiteSpace();
	}

	[Fact]
	public async Task PersonEnhancer_WithTwoSearchers_ShouldPrioritizeFirstSearcherResults()
	{
		var searcher = new PersonEnhancer([FakeSearcher, AnotherFakeSearcher]);

		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.FirstName.Should().Be("first");
	}

	[Fact]
	public async Task ProfileMerger_MergingTwoProfilesWithPopulatedLanguageLists_CanMergeSuccessfully()
	{
		var searcher = new PersonEnhancer([FakeSearcher, AnotherFakeSearcher]);
		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.Languages.Should().Contain("english");
		response.Languages.Should().Contain("french");
	}

	[Fact]
	public async Task ProfileMerger_IfFirstProfileNotFound_SecondShouldOverride()
	{
		var searcher = new PersonEnhancer([NoEnhancementSearcher, FakeSearcher]);
		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.FirstName.Should().Be("first");
	}
}