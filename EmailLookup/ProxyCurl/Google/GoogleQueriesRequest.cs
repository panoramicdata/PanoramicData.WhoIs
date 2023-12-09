namespace EmailLookup.Core.ProxyCurl.Google;

public class GoogleQueriesRequest
{
	public string Title { get; set; } = string.Empty;

	public int Count { get; set; }

	public int StartIndex { get; set; }
}