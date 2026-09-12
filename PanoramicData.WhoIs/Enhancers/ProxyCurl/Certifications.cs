using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy certifications accomplished by this user
/// </summary>
public class Certifications
{
	/// <summary>
	/// Started at
	/// </summary>
	[JsonPropertyName("starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// Ended at
	/// </summary>
	[JsonPropertyName("ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Name of the course or program
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// License number
	/// </summary>
	[JsonPropertyName("license_number")]
	public string LicenseNumber { get; set; } = string.Empty;

	/// <summary>
	/// Display source
	/// </summary>
	[JsonPropertyName("display_source")]
	public string DisplaySource { get; set; } = string.Empty;

	/// <summary>
	/// The organization body issuing this certificate
	/// </summary>
	[JsonPropertyName("authority")]
	public string Authority { get; set; } = string.Empty;

	/// <summary>
	/// URL
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}