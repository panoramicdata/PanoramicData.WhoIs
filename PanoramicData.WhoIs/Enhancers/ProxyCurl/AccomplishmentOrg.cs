using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy organizations that this user is part of
/// </summary>
public class AccomplishmentOrg
{
	/// <summary>
	/// Start date
	/// </summary>
	[JsonPropertyName("starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// End date
	/// </summary>
	[JsonPropertyName("ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Organisation name
	/// </summary>
	[JsonPropertyName("org_name")]
	public string OrgName { get; set; } = string.Empty;

	/// <summary>
	/// Title at organisation
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Description
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}