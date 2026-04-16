namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// Holds API keys and configuration values required by the ProxyCurl person enrichment pipeline.
/// </summary>
public class ProxyCurlConfig
{
	/// <summary>
	/// The Google Custom Search Engine ID (the <c>cx</c> parameter) used when searching for LinkedIn profiles.
	/// </summary>
	public string GoogleCx { get; set; } = string.Empty;

	/// <summary>
	/// The Google API key used to authenticate Custom Search API requests.
	/// </summary>
	public string GoogleKey { get; set; } = string.Empty;

	/// <summary>
	/// The API key used to authenticate requests to the ProxyCurl LinkedIn enrichment service.
	/// </summary>
	public string ProxyCurlKey { get; set; } = string.Empty;

	/// <summary>
	/// The local folder path used to cache ProxyCurl API responses, reducing repeated API calls
	/// for the same profile. Leave empty to disable caching.
	/// </summary>
	public string ProxyCurlCacheFolder { get; set; } = string.Empty;
}
