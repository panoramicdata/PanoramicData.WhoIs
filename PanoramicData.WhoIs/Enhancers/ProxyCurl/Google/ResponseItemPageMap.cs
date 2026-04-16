namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents the page map section of a Google Custom Search result item,
/// containing extracted structured metadata from the page.
/// </summary>
public class ResponseItemPageMap
{
	/// <summary>
	/// The list of Open Graph and other meta tag objects extracted from the page.
	/// </summary>
	public List<PageMapMetatags> Metatags { get; set; } = [];
}