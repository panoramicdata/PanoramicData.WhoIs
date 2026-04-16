namespace PanoramicData.WhoIs;

/// <summary>
/// A company.
/// </summary>
public class Company
{
	/// <summary>
	/// The fully-qualified domain name of the company.
	/// </summary>
	public string? DomainName { get; set; }

	/// <summary>
	/// The name of the company.
	/// </summary>
	public string? Name { get; set; }

	/// <summary>
	/// The unique domain registry identifier assigned to this domain by the registry.
	/// </summary>
	public string? RegistryDomainId { get; set; }

	/// <summary>
	/// The WHOIS server hostname used by the domain's registrar to serve registration data.
	/// </summary>
	public string? RegistrarWhoIsServer { get; set; }

	/// <summary>
	/// The URL of the registrar's website.
	/// </summary>
	public string? RegistrarUrl { get; set; }

	/// <summary>
	/// The date and time when the domain registration record was last updated.
	/// </summary>
	public DateTime? UpdatedDate { get; set; }

	/// <summary>
	/// The date and time when the domain was first registered.
	/// </summary>
	public DateTime? CreationDate { get; set; }

	/// <summary>
	/// The date and time when the domain registration is due to expire.
	/// </summary>
	public DateTime? RegistrarRegistrationExpirationDate { get; set; }

	/// <summary>
	/// The name of the accredited registrar through which this domain is registered.
	/// </summary>
	public string? Registrar { get; set; }

	/// <summary>
	/// The IANA-assigned numeric identifier for the domain's registrar.
	/// </summary>
	public string? RegistrarIanaId { get; set; }

	/// <summary>
	/// The email address to report abuse associated with this domain to the registrar.
	/// </summary>
	public string? RegistrarAbuseContactEmail { get; set; }

	/// <summary>
	/// The phone number to report abuse associated with this domain to the registrar.
	/// </summary>
	public string? RegistrarAbuseContactPhone { get; set; }

	/// <summary>
	/// Space-separated EPP status codes describing the current state of the domain (e.g. clientTransferProhibited).
	/// </summary>
	public string? DomainStatus { get; set; }

	/// <summary>
	/// The name of the organisation that registered this domain (the registrant).
	/// </summary>
	public string? RegistrantOrganization { get; set; }

	/// <summary>
	/// The state or province listed in the registrant's contact record.
	/// </summary>
	public string? RegistrantState { get; set; }

	/// <summary>
	/// The country listed in the registrant's contact record, typically as an ISO 3166-1 alpha-2 code.
	/// </summary>
	public string? RegistrantCountry { get; set; }

	/// <summary>
	/// The email address listed in the registrant's contact record.
	/// </summary>
	public string? RegistrantEmail { get; set; }

	/// <summary>
	/// The email address listed in the administrative contact record for this domain.
	/// </summary>
	public string? AdminEmail { get; set; }

	/// <summary>
	/// The LinkedIn company page URL associated with this domain, if known.
	/// </summary>
	public string? LinkedInUrl { get; set; }
}