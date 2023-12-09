using EmailLookup.Extensions;

namespace EmailLookup
{
	public class CompanySearcher(IReadOnlyCollection<string> words)
	{
		private readonly IReadOnlyCollection<string> _words = words;

		/// <summary>
		/// Uses a dictionary of English words to try to find the company name from the domain
		/// </summary>
		/// <param name="fqdnFirstPart">The first part of a domain name</param>
		/// <param name="englishWords">A dictionary of valid words</param>
		/// <returns></returns>
		private string GetCompanyNameFromDomain(string fqdnFirstPart)
		{
			// Use a tree search to find the shortest set of longest words, all of which must be in the dictionary
			// If the strategy fails, return the first part of the domain name
			var companyName = fqdnFirstPart;

			// Determine the list of words that are in the FQDN first part
			var wordsInFqdnFirstPart = _words.Where(w => fqdnFirstPart.Contains(w)).ToList();

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
}
