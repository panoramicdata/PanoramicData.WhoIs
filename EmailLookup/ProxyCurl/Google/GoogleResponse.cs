namespace EmailLookup.Core.ProxyCurl.Google;

public class GoogleResponse
{
	/// <summary>
	/// Type of search - should be customsearch#search
	/// </summary>
	public string Kind { get; set; } = string.Empty;

	public GoogleResponseUrl Url { get; set; } = new();

	/// <summary>
	/// Information on query used in search 
	/// </summary>
	public GoogleResponseQueries Queries { get; set; } = new();

	public List<GoogleResponseItems> Items { get; set; } = new();
}
