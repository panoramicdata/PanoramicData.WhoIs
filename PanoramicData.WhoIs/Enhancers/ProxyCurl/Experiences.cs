using PanoramicData.WhoIs.ProfileResult;
using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// A single entry from a user's historic work experience as reported by ProxyCurl.
/// Extends <see cref="ProfileExperiences"/> with the fields ProxyCurl supplies in addition
/// to the common shape.
/// </summary>
public class Experiences : ProfileExperiences
{
	/// <summary>
	/// Start date of the work experience
	/// </summary>
	[JsonPropertyName("starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// End date of the work experience
	/// </summary>
	[JsonPropertyName("ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Company's profile URL
	/// </summary>
	[JsonPropertyName("company_linkedin_profile_url")]
	public string CompanyLinkedinProfileUrl { get; set; } = string.Empty;

	/// <summary>
	/// Returns the source-independent view of this work experience.
	/// </summary>
	public ProfileExperiences ToProfileExperiences() => new()
	{
		Company = Company,
		Title = Title,
		Description = Description,
		Location = Location
	};
}
