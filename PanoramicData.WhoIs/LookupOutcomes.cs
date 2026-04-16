namespace PanoramicData.WhoIs;

/// <summary>
/// Enum indicating whether a searcher succeeding in populating a profile.
/// </summary>
public enum LookupOutcomes
{
	/// <summary>
	/// The lookup did not find any matching profile information.
	/// </summary>
	NotFound = 0,

	/// <summary>
	/// The lookup successfully found profile information.
	/// </summary>
	Found = 1
}