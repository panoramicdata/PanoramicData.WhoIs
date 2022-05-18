using Newtonsoft.Json;

namespace EmailLookup
{
   public class GoogleSearcher
   {
	  private readonly string _googleCx;
	  private readonly string _googleKey;
	  private readonly HttpClient client = new();

	  public GoogleSearcher(string googleCx, string googleKey)
	  {
		 _googleCx = googleCx;
		 _googleKey = googleKey;
	  }
	  public async Task<LinkedinGoogleSearchResponse?> SearchLinkedInAsync(Person person)
	  {

		 string nameQuery = Uri.EscapeDataString($"{person.FirstName} {person.LastName} {person.CompanyName}");

		 var googleApiUrl = $"https://customsearch.googleapis.com/customsearch/v1?cx={_googleCx}&q={nameQuery}&key={_googleKey}";

		 var googleStringResponse = await client
			.GetStringAsync(googleApiUrl)
			.ConfigureAwait(false);
		 var googleResponseList = JsonConvert.DeserializeObject<GoogleResponse>(googleStringResponse);

		 if (googleResponseList is null || googleResponseList.Queries.Request[0].Count <= 0)
		 {
			return null;
		 }

		 int? currentBestScore = null;
		 LinkedinGoogleSearchResponse current = new();
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
