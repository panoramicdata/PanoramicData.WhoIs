using Whois;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// An interface that interacts with the WHOIS Lookup API, which returns detailed information
/// on a company's domain information.
/// </summary>
public class WhoIsCompanyEnhancer : BasicCompanyEnhancer
{
	/// <summary>
	/// Searches the WHOIS database for information on a domain and stores that information in a
	/// Profile object, which is then returned.
	/// </summary>
	public override async Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(company, nameof(company));

		var response = await new WhoisLookup()
			.LookupAsync(company.DomainName)
			.ConfigureAwait(false);

		return response is null
			? company
			: Merge(company, ToCompany(response));
	}

	private static Company ToCompany(WhoisResponse response)
	{
		var company = new Company();

		SetIdentity(company, response);
		SetRegistrar(company, response);
		SetRegistration(company, response);
		SetRegistrant(company, response);

		return company;
	}

	private static void SetIdentity(Company company, WhoisResponse response)
	{
		company.Name = response.AdminContact?.Organization;
		company.AdminEmail = response.AdminContact?.Email;
		company.DomainName = response.DomainName.Value;
		company.RegistryDomainId = response.RegistryDomainId;
	}

	private static void SetRegistrar(Company company, WhoisResponse response)
	{
		var registrar = response.Registrar;

		company.Registrar = registrar?.Name;
		company.RegistrarUrl = registrar?.Url;
		company.RegistrarIanaId = registrar?.IanaId;
		company.RegistrarWhoIsServer = response.WhoisServer?.Value;
		company.RegistrarAbuseContactEmail = registrar?.AbuseEmail;
		company.RegistrarAbuseContactPhone = registrar?.AbuseTelephoneNumber;
	}

	private static void SetRegistration(Company company, WhoisResponse response)
	{
		company.CreationDate = response.Registered;
		company.UpdatedDate = response.Updated;
		company.RegistrarRegistrationExpirationDate = response.Expiration;
		company.DomainStatus = response.DomainStatus?.FirstOrDefault();
	}

	private static void SetRegistrant(Company company, WhoisResponse response)
	{
		var registrant = response.Registrant;

		company.RegistrantOrganization = registrant?.Organization;
		company.RegistrantState = registrant?.Address?.FirstOrDefault();
		company.RegistrantCountry = registrant?.Address?.FirstOrDefault();
		company.RegistrantEmail = registrant?.Email;
	}
}
