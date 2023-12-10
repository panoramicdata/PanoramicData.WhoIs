using Newtonsoft.Json;
using PanoramicData.WhoIs.CustomExceptions;
using PanoramicData.WhoIs.ProxyCurl;
using PanoramicData.WhoIs.ProxyCurl.Google;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// An interface that interacts with the Nubela ProxyCurl API, as well as the Google API, to
/// return information on someone derived from their LinkedIn page. Requires API keys for
/// ProxyCurl and Google Custom search, as well as a Google Custom Search CX, to work.
/// </summary>
public class ProxyCurlPersonEnhancer(ProxyCurlConfig config) : BasicPersonEnhancer, IDisposable
{
	private readonly HttpClient _client = new();
	private readonly ProxyCurlConfig _config = config;
	private bool _disposedValue;

	/// <summary>
	/// Attempts to get a LinkedIn URL from a Google Custom Search, and uses the ProxyCurl
	/// ReverseEmailLookup if the first method fails. Then uses the URL to get data from a
	/// person's LinkedIn page, stores that information in a Profile object and returns that.
	/// </summary>
	public override async Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken)
	{
		person = BasicEnhance(person);

		string linkedInUrl;
		if (!string.IsNullOrWhiteSpace(person.LinkedInUrl))
		{
			linkedInUrl = person.LinkedInUrl;
		}
		else
		{
			// Attempt to get Linked In profile url from a google search
			var googleSearchResponse = await SearchGoogleAsync(person, cancellationToken)
				.ConfigureAwait(false);

			linkedInUrl = googleSearchResponse.Url;
		}

		if (person.MailAddress is not null && !linkedInUrl.Contains("/in/"))
		{
			// try backup email searcher
			linkedInUrl = await ReverseWorkEmailLookupAsync(person.MailAddress.Address, cancellationToken)
				.ConfigureAwait(false);
		}

		// use the obtained profile url to get information using ProxyCurl endpoint
		var personFromLinkedInInformation = await GetPersonFromLinkedInUrlAsync(person, linkedInUrl, cancellationToken)
			.ConfigureAwait(false);
		if (personFromLinkedInInformation is null)
		{
			return person;
		}

		return Merge(person, personFromLinkedInInformation);
	}

	/// <summary>
	/// Uses the ProxyCurl ReverseEmailLookup endpoint to get a LinkedIn profile link from a
	/// work email.
	/// </summary>
	public async Task<string> ReverseWorkEmailLookupAsync(string address, CancellationToken cancellationToken)
	{
		try
		{
			var url = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + address;
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

			// Is it in the cache?
			var searchResponse = await _client
				.GetFromJsonAsync<LinkSearchResponse>(url, cancellationToken)
				.ConfigureAwait(false) ?? throw new FormatException("Could not deserialize.");

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
	public async Task<Person?> GetPersonFromLinkedInUrlAsync(
		Person person,
		string linkedInUrl,
		CancellationToken cancellationToken)
	{
		ArgumentNullException.ThrowIfNull(person, nameof(person));
		ArgumentNullException.ThrowIfNull(linkedInUrl, nameof(linkedInUrl));

		var address = person.MailAddress?.Address;
		if (address is null)
		{
			return person;
		}

		DetailedPersonInformation detailedPersonInformation;
		var fileInfo = new FileInfo(Path.Combine(_config.ProxyCurlCacheFolder ?? string.Empty, $"email_{address}.json"));
		if (fileInfo.Exists)
		{
			var fileContents = await File
				.ReadAllTextAsync(fileInfo.FullName, cancellationToken)
				.ConfigureAwait(false);

			// Deserialize
			detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(fileContents)
				?? throw new FormatException("Could not deserialize.");
		}
		else
		{
			var url = $"https://nubela.co/proxycurl/api/v2/linkedin?url={linkedInUrl}&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";
			_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

			try
			{
				detailedPersonInformation = await _client
					.GetFromJsonAsync<DetailedPersonInformation>(url, cancellationToken)
					.ConfigureAwait(false) ?? throw new FormatException("Could not deserialize.");
			}
			catch (Exception e)
			{
				HandleProxyCurlException(e);
				return null;
			}
		}

		if (detailedPersonInformation is null)
		{
			return new Person
			{
				MailAddress = person.MailAddress,
				LinkedInUrl = linkedInUrl,
			};
		}

		return new Person
		{
			MailAddress = person.MailAddress,
			FirstName = detailedPersonInformation.FirstName,
			LastName = detailedPersonInformation.LastName,
			BirthYear = detailedPersonInformation.BirthDate?.Year,
			LinkedInUrl = linkedInUrl,
			Country = detailedPersonInformation.Country,
			City = detailedPersonInformation.City,
			State = detailedPersonInformation.State,
			Languages = detailedPersonInformation.Languages,
			PersonalEmails = detailedPersonInformation.PersonalEmails,
			PersonalNumbers = detailedPersonInformation.PersonalNumbers,
		};
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
		var nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.Company?.Name}");
		var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_config.GoogleCx}&q={nameQuery}&key={_config.GoogleKey}";
		var googleResponseList = await _client
			.GetFromJsonAsync<GoogleResponse>(googleApiUrl, cancellationToken)
			.ConfigureAwait(false);

		if (googleResponseList is null || googleResponseList.Queries.Request[0].Count == 0)
		{
			return new GoogleSearchResponse();
		}

		// makes a score based on the likelihood of the obtained url being the linkedin profile url
		var currentBestScore = 0;
		GoogleSearchResponse current = new();
		foreach (var item in googleResponseList.Items)
		{
			var score = 0;
			var link = item.Link;
			var title = item.Title;
			var description = item.Snippet;

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