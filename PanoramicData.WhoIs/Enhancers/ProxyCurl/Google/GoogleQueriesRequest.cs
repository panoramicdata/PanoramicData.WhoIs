namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents a single request entry within the <c>queries</c> section of a Google Custom Search API response.
/// </summary>
public class GoogleQueriesRequest
{
	/// <summary>
	/// A human-readable title describing the query.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The number of results returned for this query.
	/// </summary>
	public int Count { get; set; }

	/// <summary>
	/// The 1-based index of the first result returned in this page of results.
	/// </summary>
	public int StartIndex { get; set; }
}