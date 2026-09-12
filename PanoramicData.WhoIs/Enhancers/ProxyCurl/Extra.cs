using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// Bundle of extra data on the user
/// </summary>
public class Extra
{
	/// <summary>
	/// Profile's GitHub account
	/// </summary>
	[JsonPropertyName("github_profile_id")]
	public string GithubProfileId { get; set; } = string.Empty;

	/// <summary>
	/// Profile's Facebook account
	/// </summary>
	[JsonPropertyName("facebook_profile_id")]
	public string FacebookProfileId { get; set; } = string.Empty;

	/// <summary>
	/// Profile's Twitter account
	/// </summary>
	[JsonPropertyName("twitter_profile_id")]
	public string TwitterProfileId { get; set; } = string.Empty;
}