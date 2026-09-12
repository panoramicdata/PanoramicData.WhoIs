using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy publications that this user has partook in
/// </summary>
public class Publication
{
	/// <summary>
	/// Name
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Publishing organisation body
	/// </summary>
	[JsonPropertyName("publisher")]
	public string Publisher { get; set; } = string.Empty;

	/// <summary>
	/// Date of publication
	/// </summary>
	[JsonPropertyName("published_on")]
	public ProxyCurlDate PublishedOn { get; set; } = new();

	/// <summary>
	/// Description
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// URL of publication
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}