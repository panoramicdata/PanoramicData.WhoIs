using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// Abstract base class for company enhancers, providing shared merge logic
/// that combines the known source data with newly discovered enrichment data.
/// </summary>
public abstract class BasicCompanyEnhancer : ICompanyEnhancer
{
	/// <summary>
	/// Enriches the specified <see cref="Company"/> with additional information
	/// from the data source implemented by the derived class.
	/// </summary>
	/// <param name="company">The company to enrich.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Company"/> populated with as much information as the source provides.</returns>
	public abstract Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken);

	internal static Company Merge(Company sourceCompany, Company enhancedCompany) => new()
	{
		Name = sourceCompany.Name ?? enhancedCompany.Name,
		AdminEmail = sourceCompany.AdminEmail ?? enhancedCompany.AdminEmail,
		DomainName = sourceCompany.DomainName ?? enhancedCompany.DomainName,
		RegistryDomainId = sourceCompany.RegistryDomainId ?? enhancedCompany.RegistryDomainId,
		RegistrarWhoIsServer = sourceCompany.RegistrarWhoIsServer ?? enhancedCompany.RegistrarWhoIsServer,
		RegistrarUrl = sourceCompany.RegistrarUrl ?? enhancedCompany.RegistrarUrl,
		UpdatedDate = sourceCompany.UpdatedDate ?? enhancedCompany.UpdatedDate,
		CreationDate = sourceCompany.CreationDate ?? enhancedCompany.CreationDate,
		RegistrarRegistrationExpirationDate = sourceCompany.RegistrarRegistrationExpirationDate ?? enhancedCompany.RegistrarRegistrationExpirationDate,
		Registrar = sourceCompany.Registrar ?? enhancedCompany.Registrar,
		RegistrarIanaId = sourceCompany.RegistrarIanaId ?? enhancedCompany.RegistrarIanaId,
		RegistrarAbuseContactEmail = sourceCompany.RegistrarAbuseContactEmail ?? enhancedCompany.RegistrarAbuseContactEmail,
		RegistrarAbuseContactPhone = sourceCompany.RegistrarAbuseContactPhone ?? enhancedCompany.RegistrarAbuseContactPhone,
		DomainStatus = sourceCompany.DomainStatus ?? enhancedCompany.DomainStatus,
		RegistrantOrganization = sourceCompany.RegistrantOrganization ?? enhancedCompany.RegistrantOrganization,
		RegistrantState = sourceCompany.RegistrantState ?? enhancedCompany.RegistrantState,
		RegistrantCountry = sourceCompany.RegistrantCountry ?? enhancedCompany.RegistrantCountry,
		RegistrantEmail = sourceCompany.RegistrantEmail ?? enhancedCompany.RegistrantEmail,
	};
}