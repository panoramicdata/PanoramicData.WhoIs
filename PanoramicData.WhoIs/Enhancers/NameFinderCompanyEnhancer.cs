using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Extensions;

namespace PanoramicData.WhoIs;

/// <summary>
/// A company enhancer that uses a built-in dictionary of English words to infer the
/// company name from the first segment of a domain name (e.g. "acmecorp.com" → "Acme Corp").
/// A custom word list may be supplied to override or extend the default dictionary.
/// </summary>
/// <param name="words">
/// An optional collection of words to use as the lookup dictionary.
/// When <see langword="null"/>, the built-in English word list is used.
/// </param>
public partial class NameFinderCompanyEnhancer(IReadOnlyCollection<string>? words = null) : BasicCompanyEnhancer
{
	private readonly IReadOnlyCollection<string>? _suppliedWordList = words;
	private IReadOnlyCollection<string> _words = [];
	private bool _hasLowerCased;

	/// <summary>
	/// Uses a dictionary of English words to try to find the company name from the domain.
	/// </summary>
	/// <param name="company">The company whose name should be inferred from its domain name.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Company"/> with the <see cref="Company.Name"/> populated where a match is found.</returns>
	public override Task<Company> EnhanceAsync(Company company, CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(company, nameof(company));
		if (company.DomainName is null)
		{
			return Task.FromResult(company);
		}

		var fqdnFirstPart = company.DomainName.Split('.')[0];

		var companyName = fqdnFirstPart.ToLowerInvariant();

		// Use a tree search to find the shortest set of longest words, all of which must be in the dictionary
		// If the strategy fails, return the first part of the domain name
		if (!_hasLowerCased)
		{
			_words = (_suppliedWordList ?? EnglishWords.Split('\n'))
				// Exclude swear words
				.Where(w => !w.Contains('!'))
				// Ignore characters after a slash
				.Select(w => w.Split('/')[0].ToLowerInvariant().Trim())
				.Distinct()
				.ToList();
			_hasLowerCased = true;
		}

		// TODO : Add DNS record lookup
		// TODO : Add website string search

		// Determine the list of words that are in the FQDN first part
		var wordsInFqdnFirstPart = _words
			.Select(w => w.ToLowerInvariant())
			.Where(companyName.Contains)
			.ToList();

		// If there are no words in the FQDN first part, return the first part of the domain name
		if (wordsInFqdnFirstPart.Count == 0)
		{
			return Task.FromResult(new Company
			{
				DomainName = company.DomainName,
				Name = fqdnFirstPart.ToPascalCase()
			});
		}

		companyName = GetWordListRecursive(fqdnFirstPart, out List<string> fewestContiguousWords)
			? string.Join(" ", fewestContiguousWords.Select(w => w.ToPascalCase()))
			: fqdnFirstPart;

		return Task.FromResult(new Company
		{
			DomainName = company.DomainName,
			Name = companyName
		});
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
			.Where(w => remainingString.StartsWith(w, StringComparison.Ordinal))
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
