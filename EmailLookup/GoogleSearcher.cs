using Newtonsoft.Json;
using System.Net;

namespace EmailLookup
{
   public class GoogleSearcher
   {
	  private readonly string _googleCx;
	  private readonly string _googleKey;
	  private readonly string _linkedInKey;
	  private readonly HttpClient client = new();

	  public GoogleSearcher(string googleCx, string googleKey, string linkedInKey)
	  {
		 _googleCx = googleCx;
		 _googleKey = googleKey;
         _linkedInKey = linkedInKey;
        }

	  public async Task<LinkSearchResponse?> SearchLinkedInAsync(
		 Person person,
		 CancellationToken cancellationToken
		 )
	  {
			var googleSearchResponse = await SearchGoogleAsync(person, cancellationToken)
				.ConfigureAwait(false);

			// Do we have a response?
			if (googleSearchResponse is null)
			{
				// No - return null
				return null;
			}

            string? googleUrl = googleSearchResponse.Url;

            if (!(googleUrl.Contains("/in/")))
            {
                var getProfileUrl = "https://nubela.co/proxycurl/api/linkedin/profile/resolve/email?work_email=" + person.Email;
                var profileHttpRequest = (HttpWebRequest)WebRequest.Create(getProfileUrl);
                profileHttpRequest.Headers["Authorization"] = "Bearer " + _linkedInKey;

                var profileHttpResponse = (HttpWebResponse)profileHttpRequest.GetResponse();
                using var profileStreamReader = new StreamReader(profileHttpResponse.GetResponseStream());
                var profileResult = profileStreamReader.ReadToEnd();

                LinkSearchResponse? searchResponse = JsonConvert.DeserializeObject<LinkSearchResponse>(profileResult);
                googleUrl = searchResponse.Url;

				return searchResponse;
            }
			return null;

            //var url = "https://nubela.co/proxycurl/api/v2/linkedin?url=" + googleUrl + "&fallback_to_cache=on-error&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

            //var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpRequest.Headers["Authorization"] = "Bearer " + _linkedInKey;

            //var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            //using var streamReader = new StreamReader(httpResponse.GetResponseStream());
            //var result = streamReader.ReadToEnd();

            //DetailedPersonInformation? detailedPersonInformation = JsonConvert.DeserializeObject<DetailedPersonInformation>(result);

            //return detailedPersonInformation;
        }

	  public async Task<GoogleSearchResponse?> SearchGoogleAsync(
		 Person person,
		 CancellationToken cancellationToken
		 )
	  {
		 string nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.CompanyName}");

		 var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_googleCx}&q={nameQuery}&key={_googleKey}";

		 var googleStringResponse = await client
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
			int score = 0;
			var link = item.PageMap.Metatags[0].OgUrl;
			var title = item.PageMap.Metatags[0].OgTitle;
			var description = item.PageMap.Metatags[0].OgDesc;

			if (link.Contains("/in/"))
			{
			   score += 25;
			}

			if (title.ToLower().Contains(person.FirstName))
			{
			   score += 25;
			}

			if (title.ToLower().Contains(person.LastName))
			{
			   score += 25;
			}

			if (description.ToLower().Contains(person.CompanyName.ToLower()))
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
