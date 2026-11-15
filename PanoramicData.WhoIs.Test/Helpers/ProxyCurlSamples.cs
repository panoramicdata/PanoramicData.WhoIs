using System.Net;

namespace PanoramicData.WhoIs.Test.Helpers;

internal static class ProxyCurlSamples
{
	internal const string LinkedInUrl = "https://www.linkedin.com/in/janedoe";

	internal const string ProfileJson = """
		{
		  "first_name": "Jane", "last_name": "Doe", "gender": "female", "occupation": "Engineer",
		  "country": "GB", "country_full_name": "United Kingdom", "city": "London", "state": "England",
		  "languages": ["English", "French"],
		  "personal_emails": ["jane@home.example"], "personal_numbers": ["+441234567890"],
		  "birth_date": { "day": 1, "month": 2, "year": 1990 },
		  "experiences": [{ "company": "Acme", "title": "Lead", "description": "Led", "location": "London",
		    "starts_at": { "year": 2020 }, "ends_at": { "year": 2022 }, "company_linkedin_profile_url": "u" }],
		  "education": [{ "field_of_study": "Maths", "degree_name": "BSc", "school": "Uni", "description": "Studied" }],
		  "accomplishment_honors_awards": [{ "title": "Award", "issuer": "Body", "description": "Won" }],
		  "accomplishment_courses": [{ "name": "Course", "number": "C1" }],
		  "accomplishment_projects": [{ "title": "Project", "description": "Built" }],
		  "accomplishment_organisations": [{}], "accomplishment_publications": [{}], "accomplishment_patents": [{}],
		  "accomplishment_test_scores": [{}], "volunteer_work": [{}], "certifications": [{}],
		  "people_also_viewed": [{}], "articles": [{}], "groups": [{}],
		  "recommendations": ["r"], "interests": ["i"], "connections": 5,
		  "inferred_salary": {}, "extra": {}
		}
		""";

	internal static string GoogleJson(params (string Link, string Title, string Description)[] items)
	{
		var rows = string.Join(",", items.Select(i =>
			$$$"""{"title":"{{{i.Title}}}","link":"{{{i.Link}}}","snippet":"s","pageMap":{"metatags":[{"og:title":"{{{i.Title}}}","og:url":"{{{i.Link}}}","og:description":"{{{i.Description}}}"}]}}"""));
		return $$$"""{"kind":"customsearch#search","queries":{"request":[{"title":"t","count":{{{items.Length}}},"startIndex":1}]},"items":[{{{rows}}}]}""";
	}

	internal static (HttpStatusCode Status, string Body) Route(HttpRequestMessage request, string googleBody)
	{
		var uri = request.RequestUri!.ToString();
		return uri switch
		{
			_ when uri.Contains("customsearch") => (HttpStatusCode.OK, googleBody),
			_ when uri.Contains("resolve/email") => (HttpStatusCode.OK, $$"""{"url":"{{LinkedInUrl}}"}"""),
			_ => (HttpStatusCode.OK, ProfileJson),
		};
	}
}