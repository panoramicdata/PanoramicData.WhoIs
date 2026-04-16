namespace PanoramicData.WhoIs.Interfaces;

/// <summary>
/// Defines a service that enriches a <see cref="Company"/> with additional information
/// derived from its domain name or other available data.
/// </summary>
public interface ICompanyEnhancer
{
	/// <summary>
	/// Enriches the specified company with additional data from WHOIS or another source.
	/// </summary>
	/// <param name="company">The company to enrich, identified by its domain name.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Company"/> populated with as much information as the source provides.</returns>
	Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken);
}