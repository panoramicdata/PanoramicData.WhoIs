namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents a single result item returned by the Google Custom Search API.
/// </summary>
public class GoogleResponseItems
{
	/// <summary>
	/// The type of result item, typically <c>customsearch#result</c>.
	/// </summary>
	public string Kind { get; set; } = string.Empty;

	/// <summary>
	/// The title of the web page returned as a search result.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The URL of the web page returned as a search result.
	/// </summary>
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// A short excerpt from the web page describing its content.
	/// </summary>
	public string Snippet { get; set; } = string.Empty;

	/// <summary>
	/// Structured metadata extracted from the page's Open Graph and other meta tags.
	/// </summary>
	public ResponseItemPageMap PageMap { get; set; } = new();
}