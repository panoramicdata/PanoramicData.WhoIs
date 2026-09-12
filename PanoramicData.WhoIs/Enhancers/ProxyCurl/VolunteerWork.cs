using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of historic work experiences
/// </summary>
public class VolunteerWork
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
	/// Name of volunteer activity
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Cause
	/// </summary>
	[JsonPropertyName("cause")]
	public string Cause { get; set; } = string.Empty;

	/// <summary>
	/// The company's display name
	/// </summary>
	[JsonPropertyName("company")]
	public string Company { get; set; } = string.Empty;

	/// <summary>
	/// The company's profile URL
	/// </summary>
	[JsonPropertyName("company_linkedin_profile_url")]
	public string CompanyLinkedinProfileUrl { get; set; } = string.Empty;

	/// <summary>
	/// Description
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}