using EmailLookup.ProfileResult;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmailLookup.Core.ProxyCurl.Google;

public class GoogleSearcher
{
	private readonly string _googleCx;
	private readonly string _googleKey;
	private readonly string _linkedInKey;
	private readonly HttpClient _client = new();

	public GoogleSearcher(string googleCx, string googleKey, string linkedInKey)
	{
		_googleCx = googleCx;
		_googleKey = googleKey;
		_linkedInKey = linkedInKey;
	}

	public async Task<Profile> SearchLinkedInAsync(
	   string address,
	   CancellationToken cancellationToken
	   )
	{
		Profile emptyProfile = new Profile();

		var googleSearchResponse = await SearchGoogleAsync(address, cancellationToken)
			.ConfigureAwait(false);

		// Do we have a response?
		if (googleSearchResponse is null)
		{
			// No - return empty profile
			return emptyProfile;
		}

		var googleUrl = googleSearchResponse.Url;

		if (!googleUrl.Contains("/in/"))
		{
			var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + address;
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);

			var profileResult = await _client
				.GetStringAsync(getProfileUrl, cancellationToken)
				.ConfigureAwait(false);

			LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
			if (searchResponse is null)
			{
				return emptyProfile;
			}

			googleUrl = searchResponse.Url;
		}


		var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + googleUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _linkedInKey);
		var result = await _client
			.GetStringAsync(url, cancellationToken)
			.ConfigureAwait(false);

		DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(result);
		Profile linkedInProfile = emptyProfile;
		if (detailedPersonInformation != null)
		{
			linkedInProfile = detailedPersonInformation.ToProfile();
		}

		return linkedInProfile;
	}

	public async Task<GoogleSearchResponse> SearchGoogleAsync(
	 string address,
	 CancellationToken cancellationToken
	 )
	{
		var person = new Person(address);

		var nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.CompanyName}");

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

		int currentBestScore = 0;
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

			if (title.Contains(person.FirstName, StringComparison.OrdinalIgnoreCase))
			{
				score += 25;
			}

			if (title.Contains(person.LastName, StringComparison.OrdinalIgnoreCase))
			{
				score += 25;
			}

			if (description.Contains(person.CompanyName, StringComparison.OrdinalIgnoreCase))
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
}
