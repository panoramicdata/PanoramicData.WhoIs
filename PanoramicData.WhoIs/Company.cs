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

	public string? RegistryDomainId { get; set; }

	public string? RegistrarWhoIsServer { get; set; }

	public string? RegistrarUrl { get; set; }

	public DateTime? UpdatedDate { get; set; }

	public DateTime? CreationDate { get; set; }

	public DateTime? RegistrarRegistrationExpirationDate { get; set; }

	public string? Registrar { get; set; }

	public string? RegistrarIanaId { get; set; }

	public string? RegistrarAbuseContactEmail { get; set; }

	public string? RegistrarAbuseContactPhone { get; set; }

	public string? DomainStatus { get; set; }

	public string? RegistrantOrganization { get; set; }

	public string? RegistrantState { get; set; }

	public string? RegistrantCountry { get; set; }

	public string? RegistrantEmail { get; set; }

	public string? AdminEmail { get; set; }

	public string? LinkedInUrl { get; set; }
}