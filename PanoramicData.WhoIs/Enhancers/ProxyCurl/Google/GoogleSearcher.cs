using System.Net.Http.Headers;
using System.Text.Json;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Searches Google Custom Search for a person's LinkedIn profile URL, then resolves and
/// fetches the full LinkedIn profile via the ProxyCurl API.
/// </summary>
public class GoogleSearcher : IDisposable
{
	private readonly string _googleCx;
	private readonly string _googleKey;
	private readonly string _linkedInKey;
	private readonly HttpClient _client;

	/// <summary>
	/// Creates a searcher that owns its own <see cref="HttpClient"/>.
	/// </summary>
	/// <param name="googleCx">The Google Custom Search Engine ID.</param>
	/// <param name="googleKey">The Google API key for Custom Search requests.</param>
	/// <param name="linkedInKey">The ProxyCurl API key used to resolve and fetch LinkedIn profiles.</param>
	public GoogleSearcher(string googleCx, string googleKey, string linkedInKey)
		: this(googleCx, googleKey, linkedInKey, new HttpClient())
	{
	}

	/// <summary>
	/// Creates a searcher that sends its requests through the supplied <see cref="HttpClient"/>, which this instance disposes.
	/// </summary>
	/// <param name="googleCx">The Google Custom Search Engine ID.</param>
	/// <param name="googleKey">The Google API key for Custom Search requests.</param>
	/// <param name="linkedInKey">The ProxyCurl API key used to resolve and fetch LinkedIn profiles.</param>
	/// <param name="client">The client used for all requests.</param>
	public GoogleSearcher(string googleCx, string googleKey, string linkedInKey, HttpClient client)
	{
		_googleCx = googleCx;
		_googleKey = googleKey;
		_linkedInKey = linkedInKey;
		_client = client;
	}

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

		var getProfileUrl = ProxyCurlEndpoints.ReverseEmailLookup(person.MailAddress?.Address);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);

		var profileResult = await _client
			.GetStringAsync(getProfileUrl, cancellationToken)
			.ConfigureAwait(false);

		var searchResponse = JsonSerializer.Deserialize<LinkSearchResponse>(profileResult, ProxyCurlJson.Options);
		return searchResponse?.Url ?? googleUrl;
	}

	private async Task<Person> FetchLinkedInProfileAsync(string linkedInUrl, Person person, CancellationToken cancellationToken)
	{
		var url = ProxyCurlEndpoints.Profile(linkedInUrl);
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);
		var result = await _client
			.GetStringAsync(url, cancellationToken)
			.ConfigureAwait(false);

		var detailedPersonInformation = JsonSerializer.Deserialize<DetailedPersonInformation>(result, ProxyCurlJson.Options);
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
		var googleApiUrl = ProxyCurlEndpoints.GoogleSearch(_googleCx, _googleKey, person);
		GoogleSearchResponse current = new();

		var googleStringResponse = await _client
		   .GetStringAsync(googleApiUrl, cancellationToken)
		   .ConfigureAwait(false);
		var googleResponseList = JsonSerializer.Deserialize<GoogleResponse>(googleStringResponse, ProxyCurlJson.Options);

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
		var metatags = item.PageMap.Metatags[0];
		return GoogleResultScorer.Score(metatags.OgUrl, metatags.OgTitle, metatags.OgDesc, person);
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
		if (disposing)
		{
			_client.Dispose();
		}
	}

	/// <summary>
	/// Releases all resources used by this <see cref="GoogleSearcher"/>, including the underlying <see cref="System.Net.Http.HttpClient"/>.
	/// </summary>
	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
