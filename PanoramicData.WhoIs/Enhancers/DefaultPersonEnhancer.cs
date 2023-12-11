
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

public class DefaultPersonEnhancer() : BasicPersonEnhancer
{
	public override async Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken)
	{
		person = BasicEnhance(person);

		if (person.Company is null)
		{
			return person;
		}

		foreach (var companyEnhancer in CompanyEnhancers)
		{
			person.Company = await companyEnhancer.EnhanceAsync(person.Company, cancellationToken);
		}

		return person;
	}

	public override IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers { get; } =
	[
		new NameFinderCompanyEnhancer(),
		new WhoIsCompanyEnhancer(),
	];
}
