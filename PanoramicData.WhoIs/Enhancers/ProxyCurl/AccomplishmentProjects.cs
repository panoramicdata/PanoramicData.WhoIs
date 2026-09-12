using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy projects undertaken by this user
/// </summary>
public class AccomplishmentProjects
{
	/// <summary>
	/// Start date of project
	/// </summary>
	[JsonPropertyName("starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// End date of project
	/// </summary>
	[JsonPropertyName("ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Name of the project that has been or is currently being worked on.
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Description of the project.
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}