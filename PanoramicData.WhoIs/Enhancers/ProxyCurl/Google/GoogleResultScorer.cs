namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Scores a Google Custom Search result on how likely it is to be the LinkedIn profile
/// of a given person.
/// </summary>
internal static class GoogleResultScorer
{
	private const int MatchScore = 25;

	/// <summary>
	/// Scores a single search result out of 100, awarding <see cref="MatchScore"/> for the link
	/// being a LinkedIn profile link and for each of the person's first name, last name and
	/// company name appearing in the result text.
	/// </summary>
	/// <param name="link">The result's URL.</param>
	/// <param name="title">The result's title.</param>
	/// <param name="description">The result's description or snippet.</param>
	/// <param name="person">The person being searched for.</param>
	internal static int Score(string link, string title, string description, Person person)
		=> ScoreIf(link.Contains("/in/", StringComparison.Ordinal))
			+ ScoreContains(title, person.FirstName)
			+ ScoreContains(title, person.LastName)
			+ ScoreContains(description, person.Company?.Name);

	private static int ScoreContains(string haystack, string? needle)
		=> ScoreIf(needle is not null && haystack.Contains(needle, StringComparison.OrdinalIgnoreCase));

	private static int ScoreIf(bool matched) => matched ? MatchScore : 0;
}
