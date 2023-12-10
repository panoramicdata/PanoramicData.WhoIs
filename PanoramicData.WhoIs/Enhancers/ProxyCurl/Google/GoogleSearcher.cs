using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace PanoramicData.WhoIs.ProxyCurl.Google;

public class GoogleSearcher(string googleCx, string googleKey, string linkedInKey) : IDisposable
{
	private readonly string _googleCx = googleCx;
	private readonly string _googleKey = googleKey;
	private readonly string _linkedInKey = linkedInKey;
	private readonly HttpClient _client = new();
	private bool _disposedValue;

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

		var googleUrl = googleSearchResponse.Url;

		if (!googleUrl.Contains("/in/"))
		{
			var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + person.MailAddress?.Address;
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);

			var profileResult = await _client
				.GetStringAsync(getProfileUrl, cancellationToken)
				.ConfigureAwait(false);

			LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
			if (searchResponse is null)
			{
				return person;
			}

			googleUrl = searchResponse.Url;
		}


		var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + googleUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);
		var result = await _client
			.GetStringAsync(url, cancellationToken)
			.ConfigureAwait(false);

		DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(result);
		if (detailedPersonInformation is not null)
		{
			return detailedPersonInformation.ToProfile();
		}

		return person;
	}

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

			if (
			   currentBestScore == 0 // There is no current best score
			   || // OR
			   score > currentBestScore // The current score is better than the current best score
			   )
			{
				current.Title = title;
				current.Url = link;
				current.Description = description;
				current.Score = score;
				currentBestScore = score;
			}
		}

		return current;
	}

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

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
