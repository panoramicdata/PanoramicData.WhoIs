using EmailLookup.Core.ProxyCurl.Google;
using EmailLookup.CustomExceptions;
using EmailLookup.ProfileResult;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Headers;

namespace EmailLookup.Core.ProxyCurl
{
	/// <summary>
	/// An interface that interacts with the Nubela ProxyCurl API, as well as the Google API, to
	/// return information on someone derived from their LinkedIn page. Requires API keys for
	/// ProxyCurl and Google Custom search, as well as a Google Custom Search CX, to work.
	/// </summary>
	public class ProxyCurlSearcher : IPersonSearcher
	{
		private readonly HttpClient _client = new();
		private readonly ProxyCurlConfig _config = new();

		public ProxyCurlSearcher(ProxyCurlConfig config)
		{
			_config = config;
		}

		/// <summary>
		/// Attempts to get a LinkedIn URL from a Google Custom Search, and uses the ProxyCurl
		/// ReverseEmailLookup if the first method fails. Then uses the URL to get data from a
		/// person's LinkedIn page, stores that information in a Profile object and returns that.
		/// </summary>
		public async Task<Profile> SearchAsync(Person person)
		{
			CancellationToken cancellationToken = default;

			// attempt to get linkedin profile url from a google search
			var googleSearchResponse = await SearchGoogleAsync(person, cancellationToken)
				.ConfigureAwait(false);

			var googleUrl = googleSearchResponse.Url;

			if (!googleSearchResponse.Url.Contains("/in/"))
			{
				// try backup email searcher
				googleUrl = await ReverseWorkEmailLookupAsync(person.Email, cancellationToken)
					.ConfigureAwait(false);
			}

			// use the obtained profile url to get information using proxycurl endpoint
			var detailedProfile = await PersonProfileLookupAsync(googleUrl, cancellationToken)
				.ConfigureAwait(false);

			var profile = detailedProfile.ToProfile();

			return profile;
		}

		/// <summary>
		/// Uses the ProxyCurl ReverseEmailLookup endpoint to get a LinkedIn profile link from a
		/// work email.
		/// </summary>
		public async Task<string> ReverseWorkEmailLookupAsync(string address, CancellationToken cancellationToken)
		{
			var url = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + address;

			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

			try
			{
				var result = await _client
					.GetStringAsync(url, cancellationToken)
					.ConfigureAwait(false);

				LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(result);
				if (searchResponse is null || searchResponse.Url is null)
				{
					throw new ProxyCurlException("404");
				}
				return searchResponse.Url;
			}
			catch (Exception e)
			{
				HandleProxyCurlException(e);
				return "";
			}

		}

		/// <summary>
		/// Uses a LinkedIn profile link to send a HTTP request to Nubela's ProxyCurl API, then
		/// deserialises that data into a DetailedPersonInformation object.
		/// </summary>
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
				HandleProxyCurlException(ex);
				// TODO: Log issue
				return new DetailedPersonInformation();
			}
		}

		/// <summary>
		/// Searches a Google Custom Search that only returns results from LinkedIn, with the query
		/// 'First Name + Last Name + Company Name', with the aim of getting their LinkedIn profile
		/// ID. Does this by scoring each search result based on the likelihood of that result
		/// being the correct LinkedIn profile.
		/// </summary>
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


		/// <summary>
		/// Handles any HTTP exceptions that could be thrown from ProxyCurl requests.
		/// </summary>
		public static void HandleProxyCurlException(Exception ex)
		{
			if (ex.Message.Contains("401") || ex.Message.Contains("500"))
			{
				throw new ProxyCurlException("Invalid API Key");
			}
			if (ex.Message.Contains("403"))
			{
				throw new ProxyCurlException("You have run out of credits");
			}
			if (ex.Message.Contains("404"))
			{
				throw new ProxyCurlException("The requested resource could not be found.");
			}
			if (ex.Message.Contains("429"))
			{
				throw new ProxyCurlException("Rate limited - please retry");
			}
			if (ex.Message.Contains("503"))
			{
				throw new ProxyCurlException("Enrichment failed, please retry.");
			}
		}
	}
}