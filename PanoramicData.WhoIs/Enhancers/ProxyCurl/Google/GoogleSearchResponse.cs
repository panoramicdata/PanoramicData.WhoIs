namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents the best-matching Google search result selected for further LinkedIn enrichment,
/// scored against the target person's known name and company.
/// </summary>
public class GoogleSearchResponse
{
	/// <summary>
	/// The Open Graph title of the selected search result page.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The canonical URL of the selected search result page, typically a LinkedIn profile URL.
	/// </summary>
	public string Url { get; set; } = string.Empty;

	/// <summary>
	/// The Open Graph description of the selected search result page.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// A relevance score computed by summing points awarded for matching the person's
	/// first name, last name, company, and LinkedIn URL path pattern.
	/// </summary>
	public int Score { get; set; }
}
