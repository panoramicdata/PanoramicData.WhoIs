using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents Open Graph meta tag values extracted from a Google Custom Search result's page map,
/// providing structured page metadata such as title, URL, and description.
/// </summary>
public class PageMapMetatags
{
	/// <summary>
	/// The Open Graph description meta tag value (<c>og:description</c>) of the page.
	/// </summary>
	[JsonPropertyName("og:description")]
	public string OgDesc { get; set; } = string.Empty;

	/// <summary>
	/// The Open Graph canonical URL meta tag value (<c>og:url</c>) of the page.
	/// </summary>
	[JsonPropertyName("og:url")]
	public string OgUrl { get; set; } = string.Empty;

	/// <summary>
	/// The Open Graph title meta tag value (<c>og:title</c>) of the page.
	/// </summary>
	[JsonPropertyName("og:title")]
	public string OgTitle { get; set; } = string.Empty;
}