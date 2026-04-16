namespace PanoramicData.WhoIs.ProfileResult;

/// <summary>
/// Represents an educational qualification held by a person, as reported by a profile enrichment source.
/// </summary>
public class ProfileEducation
{
	/// <summary>
	/// The academic discipline or subject area studied (e.g. Computer Science).
	/// </summary>
	public string FieldOfStudy { get; set; } = string.Empty;

	/// <summary>
	/// The name of the qualification awarded (e.g. Bachelor of Science).
	/// </summary>
	public string DegreeName { get; set; } = string.Empty;

	/// <summary>
	/// The name of the educational institution attended.
	/// </summary>
	public string School { get; set; } = string.Empty;

	/// <summary>
	/// Additional notes or description about the course of study.
	/// </summary>
	public string Description { get; set; } = string.Empty;
}
