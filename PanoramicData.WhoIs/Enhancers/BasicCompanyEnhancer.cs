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

	/// <summary>
	/// Merges two <see cref="Company"/> instances, preferring non-null values from
	/// <paramref name="sourceCompany"/> and falling back to <paramref name="enhancedCompany"/>.
	/// </summary>
	internal static Company Merge(Company sourceCompany, Company enhancedCompany)
	{
		var company = new Company();

		MergeIdentity(company, sourceCompany, enhancedCompany);
		MergeRegistrar(company, sourceCompany, enhancedCompany);
		MergeRegistration(company, sourceCompany, enhancedCompany);
		MergeRegistrant(company, sourceCompany, enhancedCompany);

		return company;
	}

	private static void MergeIdentity(Company target, Company source, Company enhanced)
	{
		target.Name = source.Name ?? enhanced.Name;
		target.AdminEmail = source.AdminEmail ?? enhanced.AdminEmail;
		target.DomainName = source.DomainName ?? enhanced.DomainName;
		target.RegistryDomainId = source.RegistryDomainId ?? enhanced.RegistryDomainId;
	}

	private static void MergeRegistrar(Company target, Company source, Company enhanced)
	{
		target.Registrar = source.Registrar ?? enhanced.Registrar;
		target.RegistrarUrl = source.RegistrarUrl ?? enhanced.RegistrarUrl;
		target.RegistrarIanaId = source.RegistrarIanaId ?? enhanced.RegistrarIanaId;
		target.RegistrarWhoIsServer = source.RegistrarWhoIsServer ?? enhanced.RegistrarWhoIsServer;
		target.RegistrarAbuseContactEmail = source.RegistrarAbuseContactEmail ?? enhanced.RegistrarAbuseContactEmail;
		target.RegistrarAbuseContactPhone = source.RegistrarAbuseContactPhone ?? enhanced.RegistrarAbuseContactPhone;
	}

	private static void MergeRegistration(Company target, Company source, Company enhanced)
	{
		target.CreationDate = source.CreationDate ?? enhanced.CreationDate;
		target.UpdatedDate = source.UpdatedDate ?? enhanced.UpdatedDate;
		target.RegistrarRegistrationExpirationDate = source.RegistrarRegistrationExpirationDate ?? enhanced.RegistrarRegistrationExpirationDate;
		target.DomainStatus = source.DomainStatus ?? enhanced.DomainStatus;
	}

	private static void MergeRegistrant(Company target, Company source, Company enhanced)
	{
		target.RegistrantOrganization = source.RegistrantOrganization ?? enhanced.RegistrantOrganization;
		target.RegistrantState = source.RegistrantState ?? enhanced.RegistrantState;
		target.RegistrantCountry = source.RegistrantCountry ?? enhanced.RegistrantCountry;
		target.RegistrantEmail = source.RegistrantEmail ?? enhanced.RegistrantEmail;
	}
}
