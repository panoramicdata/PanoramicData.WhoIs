namespace PanoramicData.WhoIs.ProfileResult;

/// <summary>
/// Represents a course completed by a person, as reported by a profile enrichment source.
/// </summary>
public class ProfileCourses
{
	/// <summary>
	/// The name or title of the course.
	/// </summary>
	public string Name { get; set; } = string.Empty;

	/// <summary>
	/// The course number or code assigned by the institution.
	/// </summary>
	public string Number { get; set; } = string.Empty;
}
