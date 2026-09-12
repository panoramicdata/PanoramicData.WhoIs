using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy honors and awards this user has won
/// </summary>
public class AccomplishmentHonorsAwards
{
	/// <summary>
	/// Title of honor/award
	/// </summary>
	[JsonPropertyName("title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Organisation body issuing this honor/award
	/// </summary>
	[JsonPropertyName("issuer")]
	public string Issuer { get; set; } = string.Empty;

	/// <summary>
	/// Date honor/award was issued on
	/// </summary>
	[JsonPropertyName("issued_on")]
	public ProxyCurlDate IssuedOn { get; set; } = new();

	/// <summary>
	/// Description
	/// </summary>
	[JsonPropertyName("description")]
	public string Description { get; set; } = string.Empty;
}