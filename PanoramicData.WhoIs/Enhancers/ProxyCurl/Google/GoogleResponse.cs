namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents the top-level response object returned by the Google Custom Search API.
/// </summary>
public class GoogleResponse
{
	/// <summary>
	/// Type of search - should be customsearch#search
	/// </summary>
	public string Kind { get; set; } = string.Empty;

	/// <summary>
	/// The URL template and type information for the Custom Search API endpoint.
	/// </summary>
	public GoogleResponseUrl Url { get; set; } = new();

	/// <summary>
	/// Information on query used in search 
	/// </summary>
	public GoogleResponseQueries Queries { get; set; } = new();

	/// <summary>
	/// The list of search result items returned by the Google Custom Search API.
	/// </summary>
	public List<GoogleResponseItems> Items { get; set; } = [];
}
