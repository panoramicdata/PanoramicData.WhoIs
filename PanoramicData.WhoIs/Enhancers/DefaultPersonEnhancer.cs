
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// The default person enhancer that runs basic email-based enrichment followed by
/// company enrichment using the <see cref="NameFinderCompanyEnhancer"/> and <see cref="WhoIsCompanyEnhancer"/>.
/// </summary>
public class DefaultPersonEnhancer : BasicPersonEnhancer
{
	/// <summary>
	/// Enriches the person using basic email-derived data and then applies each configured
	/// company enhancer to populate company details.
	/// </summary>
	/// <param name="person">The person to enrich.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Person"/> enriched with name, company, and domain data.</returns>
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

	/// <summary>
	/// The ordered set of company enhancers applied during person enrichment:
	/// first a name-finder heuristic, then a live WHOIS lookup.
	/// </summary>
	public override IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers { get; } =
	[
		new NameFinderCompanyEnhancer(),
		new WhoIsCompanyEnhancer(),
	];
}
