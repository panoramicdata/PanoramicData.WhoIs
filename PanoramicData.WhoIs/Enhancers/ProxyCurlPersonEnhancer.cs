using Newtonsoft.Json;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;
using PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;
using PanoramicData.WhoIs.Exceptions;
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

		var detailedPersonInformation = await GetDetailedPersonInfoAsync(address, linkedInUrl, cancellationToken)
			.ConfigureAwait(false);

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

	private async Task<DetailedPersonInformation?> GetDetailedPersonInfoAsync(
		string address,
		string linkedInUrl,
		CancellationToken cancellationToken)
	{
		var fileInfo = new FileInfo(Path.Combine(_config.ProxyCurlCacheFolder ?? string.Empty, $"email_{address}.json"));
		if (fileInfo.Exists)
		{
			var fileContents = await File
				.ReadAllTextAsync(fileInfo.FullName, cancellationToken)
				.ConfigureAwait(false);

			return JsonConvert.DeserializeObject<DetailedPersonInformation>(fileContents)
				?? throw new FormatException("Could not deserialize.");
		}

		var url = $"https://nubela.co/proxycurl/api/v2/linkedin?url={linkedInUrl}&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";
		_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _config.ProxyCurlKey);

		try
		{
			return await _client
				.GetFromJsonAsync<DetailedPersonInformation>(url, cancellationToken)
				.ConfigureAwait(false) ?? throw new FormatException("Could not deserialize.");
		}
		catch (Exception e)
		{
			HandleProxyCurlException(e);
			return null;
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
			var score = ScoreGoogleItem(item, person);

			if (score > currentBestScore)
			{
				current.Title = item.Title;
				current.Url = item.Link;
				current.Description = item.Snippet;
				current.Score = score;
				currentBestScore = score;
			}
		}

		return current;
	}

	private static int ScoreGoogleItem(GoogleResponseItems item, Person person)
	{
		var score = 0;

		if (item.Link.Contains("/in/"))
		{
			score += 25;
		}

		if (person.FirstName is not null && item.Title.Contains(person.FirstName, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		if (person.LastName is not null && item.Title.Contains(person.LastName, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		if (person.Company?.Name is not null && item.Snippet.Contains(person.Company.Name, StringComparison.OrdinalIgnoreCase))
		{
			score += 25;
		}

		return score;
	}


	private static readonly Dictionary<string, string> _httpErrorMessages = new()
	{
		["401"] = "Invalid API Key",
		["500"] = "Invalid API Key",
		["403"] = "You have run out of credits",
		["404"] = "The requested resource could not be found.",
		["429"] = "Rate limited - please retry",
		["503"] = "Enrichment failed, please retry.",
	};

	/// <summary>
	/// Handles any HTTP exceptions that could be thrown from ProxyCurl requests.
	/// </summary>
	public static void HandleProxyCurlException(Exception ex)
	{
		foreach (var (code, message) in _httpErrorMessages)
		{
			if (ex.Message.Contains(code))
			{
				throw new ProxyCurlException(message);
			}
		}
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
	/// Releases all resources used by this <see cref="ProxyCurlPersonEnhancer"/>, including the underlying <see cref="System.Net.Http.HttpClient"/>.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}