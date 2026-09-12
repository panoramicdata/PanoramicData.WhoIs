using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy patents won by this user
/// </summary>
public class AccomplishmentPatents
{
	/// <summary>
	/// Title of patent
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Organisation body that issued the patent
	/// </summary>
	[JsonPropertyName("issuer")]
	public string Issuer { get; set; } = string.Empty;

	/// <summary>
	/// Date of patent issuance
	/// </summary>
	[JsonPropertyName("issued_on")]
	public ProxyCurlDate IssuedOn { get; set; } = new();

	/// <summary>
	/// Application number of the patent
	/// </summary>
	[JsonPropertyName("application_number")]
	public string ApplicationNumber { get; set; } = string.Empty;

	/// <summary>
	/// Numerical representation that identifies the patent
	/// </summary>
	[JsonPropertyName("patent_number")]
	public string PatentNumber { get; set; } = string.Empty;

	/// <summary>
	/// Url
	/// </summary>
	[JsonPropertyName("url")]
	public string Url { get; set; } = string.Empty;
}