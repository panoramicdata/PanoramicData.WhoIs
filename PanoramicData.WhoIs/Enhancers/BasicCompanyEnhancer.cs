using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

public abstract class BasicCompanyEnhancer : ICompanyEnhancer
{
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