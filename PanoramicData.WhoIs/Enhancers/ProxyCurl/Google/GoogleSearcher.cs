using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Searches Google Custom Search for a person's LinkedIn profile URL, then resolves and
/// fetches the full LinkedIn profile via the ProxyCurl API.
/// </summary>
/// <param name="googleCx">The Google Custom Search Engine ID.</param>
/// <param name="googleKey">The Google API key for Custom Search requests.</param>
/// <param name="linkedInKey">The ProxyCurl API key used to resolve and fetch LinkedIn profiles.</param>
public class GoogleSearcher(string googleCx, string googleKey, string linkedInKey) : IDisposable
{
	private readonly string _googleCx = googleCx;
	private readonly string _googleKey = googleKey;
	private readonly string _linkedInKey = linkedInKey;
	private readonly HttpClient _client = new();
	private bool _disposedValue;

	/// <summary>
	/// Searches for the person's LinkedIn profile URL via Google Custom Search and, if found,
	/// fetches the full profile from ProxyCurl.
	/// </summary>
	/// <param name="person">The person whose LinkedIn profile should be located.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Person"/> enriched with LinkedIn profile data, or the original person if no match is found.</returns>
	public async Task<Person> SearchLinkedInAsync(
	   Person person,
	   CancellationToken cancellationToken
	   )
	{
		ArgumentNullException.ThrowIfNull(person, nameof(person));

		// We need an email address to search
		if (person.MailAddress is not null)
		{
			return person;
		}

		var googleSearchResponse = await SearchGoogleAsync(person, cancellationToken)
			.ConfigureAwait(false);

		// Do we have a response?
		if (googleSearchResponse is null)
		{
			// No - return empty profile
			return person;
		}

		var googleUrl = await ResolveLinkedInUrlAsync(googleSearchResponse.Url, person, cancellationToken)
			.ConfigureAwait(false);

		return await FetchLinkedInProfileAsync(googleUrl, person, cancellationToken)
			.ConfigureAwait(false);
	}

	private async Task<string> ResolveLinkedInUrlAsync(string googleUrl, Person person, CancellationToken cancellationToken)
	{
		if (googleUrl.Contains("/in/"))
		{
			return googleUrl;
		}

		var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + person.MailAddress?.Address;
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);

		var profileResult = await _client
			.GetStringAsync(getProfileUrl, cancellationToken)
			.ConfigureAwait(false);

		var searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
		return searchResponse?.Url ?? googleUrl;
	}

	private async Task<Person> FetchLinkedInProfileAsync(string linkedInUrl, Person person, CancellationToken cancellationToken)
	{
		var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + linkedInUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);
		var result = await _client
			.GetStringAsync(url, cancellationToken)
			.ConfigureAwait(false);

		var detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(result);
		return detailedPersonInformation?.ToProfile() ?? person;
	}

	/// <summary>
	/// Searches Google Custom Search for the best-matching LinkedIn profile result for the given person.
	/// Results are scored on how well the name and company match the page's Open Graph metadata.
	/// </summary>
	/// <param name="person">The person to search for.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>The highest-scoring <see cref="GoogleSearchResponse"/>, or an empty response if no results are found.</returns>
	public async Task<GoogleSearchResponse> SearchGoogleAsync(Person person, CancellationToken cancellationToken)
	{
		var nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.Company?.Name}");

		var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_googleCx}&q={nameQuery}&key={_googleKey}";
		GoogleSearchResponse current = new();

		var googleStringResponse = await _client
		   .GetStringAsync(googleApiUrl, cancellationToken)
		   .ConfigureAwait(false);
		var googleResponseList = JsonConvert.DeserializeObject<GoogleResponse>(googleStringResponse);

		if (googleResponseList is null || googleResponseList.Queries.Request[0].Count <= 0)
		{
			return current;
		}

		var currentBestScore = 0;
		foreach (var item in googleResponseList.Items)
		{
			var score = ScoreGoogleItem(item, person);

			if (currentBestScore == 0 || score > currentBestScore)
			{
				current.Title = item.PageMap.Metatags[0].OgTitle;
				current.Url = item.PageMap.Metatags[0].OgUrl;
				current.Description = item.PageMap.Metatags[0].OgDesc;
				current.Score = score;
				currentBestScore = score;
			}
		}

		return current;
	}

	private static int ScoreGoogleItem(GoogleResponseItems item, Person person)
	{
		var score = 0;
		var link = item.PageMap.Metatags[0].OgUrl;
		var title = item.PageMap.Metatags[0].OgTitle;
		var description = item.PageMap.Metatags[0].OgDesc;

		if (link.Contains("/in/"))
		{
			score += 25;
		}

		if (person.FirstName is not null && title.Contains(person.FirstName, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		if (person.LastName is not null && title.Contains(person.LastName, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		if (person.Company?.Name is not null && description.Contains(person.Company.Name, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		return score;
	}

	/// <summary>
	/// Releases managed and unmanaged resources held by this instance.
	/// </summary>
	/// <param name="disposing">
	/// <see langword="true"/> to release both managed and unmanaged resources;
	/// <see langword="false"/> to release only unmanaged resources.
	/// </param>
	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				_client.Dispose();
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			_disposedValue = true;
		}
	}

	/// <summary>
	/// Releases all resources used by this <see cref="GoogleSearcher"/>, including the underlying <see cref="System.Net.Http.HttpClient"/>.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
