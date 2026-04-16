namespace PanoramicData.WhoIs.ProfileResult;

/// <summary>
/// Represents a period of work experience held by a person, as reported by a profile enrichment source.
/// </summary>
public class ProfileExperiences
{
	/// <summary>
	/// The name of the employer or organisation.
	/// </summary>
	public string Company { get; set; } = string.Empty;

	/// <summary>
	/// The job title held during this period of employment.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// A description of the responsibilities and achievements in this role.
	/// </summary>
	public string Description { get; set; } = string.Empty;

	/// <summary>
	/// The geographic location (city, country, or region) where this role was based.
	/// </summary>
	public string Location { get; set; } = string.Empty;
}
