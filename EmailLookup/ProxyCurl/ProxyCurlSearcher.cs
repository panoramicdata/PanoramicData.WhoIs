﻿using EmailLookup.Core.ProxyCurl.Google;
using EmailLookup.CustomExceptions;
using EmailLookup.ProfileResult;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace EmailLookup.Core.ProxyCurl
{
	public class ProxyCurlSearcher : IPersonSearcher
	{
		private readonly HttpClient _client = new();
		private readonly ProxyCurlConfig _config = new();

		public ProxyCurlSearcher(string googleCx, string googleKey, string proxyCurlKey)
		{
			_config.GoogleCx = googleCx;
			_config.GoogleKey = googleKey;
			_config.ProxyCurlKey = proxyCurlKey;
		}

		public ProxyCurlSearcher(ProxyCurlConfig config)
		{
			_config = config;
		}

		public async Task<Profile> SearchAsync(Person person)
		{
			CancellationToken cancellationToken = default;

			// attempt to get linkedin profile url from a google search
			var googleSearchResponse = await SearchGoogleAsync(person, cancellationToken)
				.ConfigureAwait(false);

			var googleUrl = "not found";

			if (googleSearchResponse is null || (googleSearchResponse is not null && !googleSearchResponse.Url.Contains("/in/")))
			{
				// try backup email searcher
				googleUrl = await ReverseWorkEmailLookupAsync(person.Email, cancellationToken)
					.ConfigureAwait(false);

				if (googleUrl.Equals("not found", StringComparison.Ordinal))
				{
					return new Profile()
					{
						Outcome = LookupOutcomes.NotFound
					};
				}
			}
			if (googleSearchResponse is not null)
			{
				googleUrl = googleSearchResponse.Url;
			}

			if (!googleUrl.Contains("/in/"))
			{
				var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + person.Email;
				_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

				var profileResult = await _client
					.GetStringAsync(getProfileUrl, cancellationToken)
					.ConfigureAwait(false);

				LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
				if (searchResponse is null)
				{
					return new Profile();
				}

				googleUrl = searchResponse.Url;
			}

			// use the obtained profile url to get information using proxycurl endpoint
			var detailedProfile = await PersonProfileLookupAsync(googleUrl, cancellationToken)
				.ConfigureAwait(false);

			//if (detailedProfile is null)
			//{
			//	return new Profile()
			//	{
			//		Outcome = LookupOutcomes.NotFound
			//	};
			//}

			var profile = detailedProfile.ToProfile();

			return profile;
		}

		public async Task<string> ReverseWorkEmailLookupAsync(string address, CancellationToken cancellationToken)
		{
			var url = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + address;

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

			var result = await _client
				.GetStringAsync(url, cancellationToken)
				.ConfigureAwait(false);

			LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(result);
			if (searchResponse is null || searchResponse.Url is null)
			{
				return "not found";
			}
			return searchResponse.Url;
		}

		public async Task<DetailedPersonInformation> PersonProfileLookupAsync(string googleUrl, CancellationToken cancellationToken)
		{

			var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + googleUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);
			try
			{
				var result = await _client
					.GetStringAsync(url, cancellationToken)
					.ConfigureAwait(false);

				DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation?>(result);

				if (detailedPersonInformation is null)
				{
					return new DetailedPersonInformation();
				}
				return detailedPersonInformation;
			}
			catch (Exception ex)
			{
				if (ex.Message.Contains("401") || ex.Message.Contains("500"))
				{
					throw new ProxyCurlException("Invalid API Key");
				}
				if (ex.Message.Contains("404"))
				{
					throw new ProxyCurlException("No LinkedIn profile found.");
				}
				if (ex.Message.Contains("429"))
				{
					throw new ProxyCurlException("Rate limited - please retry");
				}
				// TODO: Log issue
				return new DetailedPersonInformation();
			}
		}

		public async Task<GoogleSearchResponse> SearchGoogleAsync(
			Person person,
			CancellationToken cancellationToken
		 )
		{
			// this is redundant and will be removed
			var nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.CompanyName}");

			var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_config.GoogleCx}&q={nameQuery}&key={_config.GoogleKey}";

			var googleStringResponse = await _client
			   .GetStringAsync(googleApiUrl, cancellationToken)
			   .ConfigureAwait(false);
			var googleResponseList = JsonConvert.DeserializeObject<GoogleResponse>(googleStringResponse);

			if (googleResponseList is null || googleResponseList.Queries.Request[0].Count <= 0)
			{
				return new GoogleSearchResponse();
			}

			// makes a score based on the likelihood of the obtained url being the linkedin profile url
			var currentBestScore = 0;
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

				if (score > currentBestScore // The current score is better than the current best score
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
