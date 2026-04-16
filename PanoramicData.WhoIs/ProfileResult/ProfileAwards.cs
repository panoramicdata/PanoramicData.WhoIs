namespace PanoramicData.WhoIs.ProfileResult;

/// <summary>
/// Represents a honour or award received by a person, as reported by a profile enrichment source.
/// </summary>
public class ProfileAwards
{
	/// <summary>
	/// The title or name of the award.
	/// </summary>
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// The organisation or body that issued the award.
	/// </summary>
	public string Issuer { get; set; } = string.Empty;

	/// <summary>
	/// A description of the award and the reason it was granted.
	/// </summary>
	public string Description { get; set; } = string.Empty;
}
