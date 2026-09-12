using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of LinkedIn groups that this user is a part of
/// </summary>
public class Groups
{
	/// <summary>
	/// Name of the group
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// URL to LinkedIn group
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}