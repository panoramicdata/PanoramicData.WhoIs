using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// A list of other LinkedIn profiles closely related to this user
/// </summary>
public class PeopleAlsoViewed
{
	/// <summary>
	/// URL of the profile
	/// </summary>
	[JsonPropertyName("link")]
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// Name
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Summary
	/// </summary>
	[JsonPropertyName("summary")]
	public string Summary { get; set; } = string.Empty;

	/// <summary>
	/// Location
	/// </summary>
	[JsonPropertyName("location")]
	public string Location { get; set; } = string.Empty;
}