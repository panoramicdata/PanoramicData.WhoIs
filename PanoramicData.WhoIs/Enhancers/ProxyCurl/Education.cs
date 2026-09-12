using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of user's educational background
/// </summary>
public class Education
{
	/// <summary>
	/// Start date of the education
	/// </summary>
	[JsonPropertyName("starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// End date of the education
	/// </summary>
	[JsonPropertyName("ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Field of study
	/// </summary>
	[JsonPropertyName("field_of_study")]
	public string FieldOfStudy { get; set; } = string.Empty;

	/// <summary>
	/// Degree name
	/// </summary>
	[JsonPropertyName("degree_name")]
	public string DegreeName { get; set; } = string.Empty;

	/// <summary>
	/// School
	/// </summary>
	[JsonPropertyName("school")]
	public string School { get; set; } = string.Empty;

	/// <summary>
	/// School profile URL
	/// </summary>
	[JsonPropertyName("school_linkedin_profile_url")]
	public string SchoolLinkedInProfileUrl { get; set; } = string.Empty;

	/// <summary>
	/// Description
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}