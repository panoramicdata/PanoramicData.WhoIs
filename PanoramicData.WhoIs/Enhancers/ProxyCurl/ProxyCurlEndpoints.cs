namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// Builds the request URLs shared by the ProxyCurl and Google Custom Search clients.
/// </summary>
internal static class ProxyCurlEndpoints
{
	private const string ProfileQuery = "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

	internal static string ReverseEmailLookup(string? address)
		=> "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + address;

	internal static string Profile(string linkedInUrl)
		=> "https://nubela.co/proxycurl/api/v2/linkedin?url=" + linkedInUrl + ProfileQuery;

	internal static string GoogleSearch(string cx, string key, Person person)
		=> $"https://customsearch.googleapis.com/customsearch/v1?cx={cx}&q={Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.Company?.Name}")}&key={key}";
}