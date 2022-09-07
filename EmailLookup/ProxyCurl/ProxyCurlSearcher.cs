﻿using EmailLookup.Core.ProxyCurl.Google;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace EmailLookup.Core.ProxyCurl
{
	public class ProxyCurlSearcher : IPersonSearcher
	{
		private readonly HttpClient _client = new();
		private readonly ProxyCurlConfig _config = new();

		public ProxyCurlSearcher(string googleCx, string googleKey, string linkedInKey)
		{
			_config.GoogleCx = googleCx;
			_config.GoogleKey = googleKey;
			_config.ProxyCurlKey = linkedInKey;
		}

		public ProxyCurlSearcher(ProxyCurlConfig config)
		{
			_config = config;
		}

		public async Task<Profile?> SearchAsync(Person person)
		{
			CancellationToken cancellationToken = default;

			var googleSearchResponse = await SearchGoogleAsync(person.Email, cancellationToken)
				.ConfigureAwait(false);
			if (googleSearchResponse is null)
			{
				return null;
			}

			var googleUrl = googleSearchResponse.Url;
			if (!googleUrl.Contains("/in/"))
			{
				googleUrl = await ReverseWorkEmailLookupAsync(person.Email, cancellationToken)
					.ConfigureAwait(false);
				if (googleUrl is null)
				{
					return null;
				}
			}

			var detailedProfile = await PersonProfileLookupAsync(googleUrl, cancellationToken)
				.ConfigureAwait(false);

			if (detailedProfile is null)
			{
				return null;
			}

			var profile = detailedProfile.ToProfile();

			return profile;
		}

		public async Task<string?> ReverseWorkEmailLookupAsync(string address, CancellationToken cancellationToken)
		{


			LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
			if (searchResponse is null)
			{
				return null;
			}
			return searchResponse.Url;
		}

		public async Task<DetailedPersonInformation?> PersonProfileLookupAsync(string googleUrl, CancellationToken cancellationToken)
		{
			var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + googleUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

			var result = await _client
				.GetStringAsync(url, cancellationToken)
				.ConfigureAwait(false);

			DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation?>(result);

			if (detailedPersonInformation is null)
			{
				return null;
			}
			return detailedPersonInformation;
		}

		public async Task<GoogleSearchResponse?> SearchGoogleAsync(
		 string address,
		 CancellationToken cancellationToken
		 )
		{
			var person = new Person(address);

			var nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.CompanyName}");

			var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_config.GoogleCx}&q={nameQuery}&key={_config.GoogleKey}";

			var googleStringResponse = await _client
			   .GetStringAsync(googleApiUrl, cancellationToken)
			   .ConfigureAwait(false);
			var googleResponseList = JsonConvert.DeserializeObject<GoogleResponse>(googleStringResponse);

			if (googleResponseList is null || googleResponseList.Queries.Request[0].Count <= 0)
			{
				return null;
			}

			int? currentBestScore = null;
			GoogleSearchResponse current = new();
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
				   currentBestScore is null // There is no current best score
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
}
