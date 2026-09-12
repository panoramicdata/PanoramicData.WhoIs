using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy courses partook by this user
/// </summary>
public class AccomplishmentCourses
{
	/// <summary>
	/// Name of course
	/// </summary>
	[JsonPropertyName("name")]
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// Numerical representation of the course
	/// </summary>
	[JsonPropertyName("number")]
	public string Number { get; set; } = string.Empty;
}