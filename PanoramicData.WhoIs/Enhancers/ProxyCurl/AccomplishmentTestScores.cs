using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy test scores accomplished by this user
/// </summary>
public class AccomplishmentTestScores
{
	/// <summary>
	/// Title of course for which test score was derived from
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Test score
	/// </summary>
	[JsonPropertyName("score")]
	public string Score { get; set; } = string.Empty;

	/// <summary>
	/// Date test was assessed
	/// </summary>
	[JsonPropertyName("date_on")]
	public ProxyCurlDate DateOn { get; set; } = new();

	/// <summary>
	/// Description of the test score
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}