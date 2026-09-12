using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of content-based articles posted by this user
/// </summary>
public class Articles
{
	/// <summary>
	/// Title
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Link
	/// </summary>
	[JsonPropertyName("link")]
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// Date of publishing
	/// </summary>
	[JsonPropertyName("published_date")]
	public ProxyCurlDate PublishedDate { get; set; } = new();

	/// <summary>
	/// Author
	/// </summary>
	[JsonPropertyName("author")]
	public string Author { get; set; } = string.Empty;
}