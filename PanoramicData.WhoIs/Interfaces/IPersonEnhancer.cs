namespace PanoramicData.WhoIs.Interfaces;

/// <summary>
/// Defines a service that enriches a <see cref="Person"/> with additional information
/// from an external data source such as LinkedIn or a WHOIS registry.
/// </summary>
public interface IPersonEnhancer
{
	/// <summary>
	/// Enhances the person.
	/// </summary>
	Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken);

	/// <summary>
	/// The collection of company enhancers used by this person enhancer to enrich
	/// company information discovered during person enrichment.
	/// </summary>
	IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers { get; }
}