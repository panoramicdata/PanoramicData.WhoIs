using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents the response from the ProxyCurl email-to-LinkedIn profile resolution endpoint,
/// containing the resolved LinkedIn profile URL.
/// </summary>
public class LinkSearchResponse
{
	/// <summary>
	/// The resolved LinkedIn profile URL for the email address that was queried.
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}