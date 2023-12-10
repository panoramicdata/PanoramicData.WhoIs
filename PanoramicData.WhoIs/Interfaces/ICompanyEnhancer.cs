namespace PanoramicData.WhoIs.Interfaces;

public interface ICompanyEnhancer
{
	/// <summary>
	/// FInd a company based on the domain name.
	/// </summary>
	/// <param name="domainName">The domain name (e.g. panoramicdata.com)</param>
	/// <returns></returns>
	Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken);
}