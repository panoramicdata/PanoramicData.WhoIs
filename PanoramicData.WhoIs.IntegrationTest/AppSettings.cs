namespace PanoramicData.WhoIs.IntegrationTest;

public class AppSettings
{
	public string? GoogleCx { get; set; }

	public string? GoogleKey { get; set; }

	public string? ProxyCurlKey { get; set; }

	public string? ProxyCurlCacheFolder { get; set; }

	public string ValidEmailAddress { get; set; } = string.Empty;

	public string ValidFirstName { get; set; } = string.Empty;

	public string ValidProfileUrl { get; set; } = string.Empty;
}