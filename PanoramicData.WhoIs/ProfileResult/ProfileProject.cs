namespace PanoramicData.WhoIs.ProfileResult;

/// <summary>
/// Represents a project that a person has worked on, as reported by a profile enrichment source.
/// </summary>
public class ProfileProject
{
	/// <summary>
	/// The title or name of the project.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// A description of the project, its goals, and the person's contribution.
	/// </summary>
	public string Description { get; set; } = string.Empty;
}
