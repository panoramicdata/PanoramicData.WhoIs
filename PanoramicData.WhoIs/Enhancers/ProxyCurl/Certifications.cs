using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy certifications accomplished by this user
/// </summary>
[DataContract]
public class Certifications
{
	/// <summary>
	/// Started at
	/// </summary>
	[DataMember(Name = "starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// Ended at
	/// </summary>
	[DataMember(Name = "ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Name of the course or program
	/// </summary>
	[DataMember(Name = "name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// License number
	/// </summary>
	[DataMember(Name = "license_number")]
	public string LicenseNumber { get; set; } = string.Empty;

	/// <summary>
	/// Display source
	/// </summary>
	[DataMember(Name = "display_source")]
	public string DisplaySource { get; set; } = string.Empty;

	/// <summary>
	/// The organization body issuing this certificate
	/// </summary>
	[DataMember(Name = "authority")]
	public string Authority { get; set; } = string.Empty;

	/// <summary>
	/// URL
	/// </summary>
	[DataMember(Name = "url")]
	public string Url { get; set; } = string.Empty;
}