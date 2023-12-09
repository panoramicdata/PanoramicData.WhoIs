using EmailLookup.Extensions;

namespace EmailLookup;

public partial class CompanySearcher(IReadOnlyCollection<string>? words = null)
{
	private readonly IReadOnlyCollection<string>? _suppliedWordList = words;
	private IReadOnlyCollection<string> _words = [];
	private bool _hasLowerCased;

	/// <summary>
	/// Uses a dictionary of English words to try to find the company name from the domain
	/// </summary>
	/// <param name="fqdnFirstPart">The first part of a domain name</param>
	/// <param name="englishWords">A dictionary of valid words</param>
	/// <returns></returns>
	public string GetCompanyNameFromDomain(string fqdnFirstPart)
	{
		ArgumentNullException.ThrowIfNull(fqdnFirstPart, nameof(fqdnFirstPart));

		var companyName = fqdnFirstPart.ToLowerInvariant();

		// Use a tree search to find the shortest set of longest words, all of which must be in the dictionary
		// If the strategy fails, return the first part of the domain name
		if (!_hasLowerCased)
		{
			_words = (_suppliedWordList ?? EnglishWords.Split('\n'))
				// Exclude swear words
				.Where(w => !w.Contains('!'))
				// Ignore characters after a slash
				.Select(w => w.Split('/')[0].ToLowerInvariant())
				.ToList();
			_hasLowerCased = true;
		}

		// Determine the list of words that are in the FQDN first part
		var wordsInFqdnFirstPart = _words
			.Select(w => w.ToLowerInvariant())
			.Where(companyName.Contains)
			.ToList();

		// If there are no words in the FQDN first part, return the first part of the domain name
		if (wordsInFqdnFirstPart.Count == 0)
		{
			return fqdnFirstPart;
		}


		return GetWordListRecursive(
			fqdnFirstPart,
			out List<string> fewestContiguousWords
		)
			? string.Join(" ", fewestContiguousWords.Select(w => w.ToPascalCase()))
			: fqdnFirstPart;
	}

	private bool GetWordListRecursive(
		string remainingString,
		out List<string> fewestContiguousWords)
	{
		fewestContiguousWords = [];

		// If there are no words left, return true
		if (remainingString.Length == 0)
		{
			return true;
		}

		var wordsThatStartTheRemainingString = _words
			.Where(w => w.StartsWith(remainingString[0]))
			.OrderByDescending(w => w.Length)
			.ToList();

		// If there are no words that start the remaining string, return false
		if (wordsThatStartTheRemainingString.Count == 0)
		{
			return false;
		}

		// Try each word in turn
		foreach (var word in wordsThatStartTheRemainingString)
		{
			// If the word is longer than the remaining string, skip it
			if (word.Length > remainingString.Length)
			{
				continue;
			}

			// If the word is the same as the remaining string, return true
			if (word == remainingString)
			{
				fewestContiguousWords.Add(word);
				return true;
			}

			// If the word is shorter than the remaining string, recurse
			if (word.Length < remainingString.Length)
			{
				if (GetWordListRecursive(
					remainingString[word.Length..],
					out List<string> remainingWords)
				)
				{
					fewestContiguousWords.Add(word);
					fewestContiguousWords.AddRange(remainingWords);
					return true;
				}
			}
		}

		return false;
	}
}
