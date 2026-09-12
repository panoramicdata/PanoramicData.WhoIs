using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// Date
/// </summary>
public class ProxyCurlDate
{
	/// <summary>
	/// Day
	/// </summary>
	[JsonPropertyName("day")]
	public int Day { get; set; }

	/// <summary>
	/// Month
	/// </summary>
	[JsonPropertyName("month")]
	public int Month { get; set; }

	/// <summary>
	/// Year
	/// </summary>
	[JsonPropertyName("year")]
	public int Year { get; set; }
}