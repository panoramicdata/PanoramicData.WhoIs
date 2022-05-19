using HtmlAgilityPack;
using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Mail;
using Whois;
using Newtonsoft.Json;

namespace EmailLookup
{
    public class Program
    {
        private static HttpClient client = new HttpClient();

        private static async Task<WhoisResponse> GetWhoIsResponseAsync(string domain) 
            => await new WhoisLookup().LookupAsync(domain).ConfigureAwait(false);

        public static async Task Main()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .Build();

            MailAddress mailAddress;

            while (true)
            {
                Console.WriteLine("Please enter a valid email address.");
                var inputtedEmail = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(inputtedEmail))
                {
                    Console.WriteLine("Please input something!");
                    continue;
                }

                try
                {
                    mailAddress = new MailAddress(inputtedEmail.Trim());
                    break;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Invalid email format - please try again.");
                }
            }

            var whoIsResponse = await GetWhoIsResponseAsync(mailAddress.Host)
                .ConfigureAwait(false);

            Console.WriteLine(mailAddress.Host);
            String user = mailAddress.User;
            String firstName = user.Substring(0, user.IndexOf("."));
            String lastName = user.Substring(user.IndexOf(".") + 1);

            String companyName = mailAddress.Host.Substring(0, mailAddress.Host.IndexOf("."));

            //Console.WriteLine(whoIsResponse.Content);

            var googleCx = configuration["AppSettings:GOOGLE_CX"];
            var googleKey = configuration["AppSettings:GOOGLE_KEY"];
            var proxyCurlKey = configuration["AppSettings:PROXYCURL_KEY"];

            LinkedinGoogleSearchResponse? linkedinSearchResponse = await GoogleSearch(firstName, lastName, companyName, googleCx, googleKey);

            LinkedinProfileData? test = await GetDataFromUrlProxyCurl("https://www.linkedin.com/in/davidbond/", proxyCurlKey);
        }

        public static async Task<LinkedinProfileData?> GetDataFromUrlProxyCurl(string profileUrl, string proxyCurlKey)
        {
            string queryUrl = "https://nubela.co/proxycurl/api/v2/linkedin?url=" +
                profileUrl +
                "&use_cache=if-present&skills=include&inferred_salary=include&personal_email=include&personal_contact_number=include&twitter_profile_id=include&facebook_profile_id=include&github_profile_id=include&extra=include";

            var request = (HttpWebRequest)WebRequest.Create(queryUrl);
            request.Headers["Authorization"] = "Bearer " + proxyCurlKey;
            var response = (HttpWebResponse)request.GetResponse();
            using (var streamReader = new StreamReader(response.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
            }
            Console.WriteLine(response.StatusCode);

            return null;
        }

        public static async Task<LinkedinGoogleSearchResponse?> GoogleSearch(string firstName, string lastName, string companyName, string googleCx, string googleKey)
        {
            
            String nameQuery = firstName + "%20" + lastName + "%20" + companyName;

            var googleApiUrl =
                "https://customsearch.googleapis.com/customsearch/v1?cx=" + googleCx + "&q=" + nameQuery + "&key=" + googleKey;

            var googleTask = client.GetStreamAsync(googleApiUrl);
            var googleStringTask = client.GetStringAsync(googleApiUrl);
            var googleStringResponse = await googleStringTask;
            var googleResponseList = JsonConvert.DeserializeObject<GoogleResponse>(googleStringResponse);

            string? link = null;
            string? title = null;
            string? desc = null;
            int score = 0;

            LinkedinGoogleSearchResponse current = new LinkedinGoogleSearchResponse();
            int currentBestScore = 0;

            if (googleResponseList.Queries.Request[0].Count > 0)
            {

                for (int i = googleResponseList.Queries.Request[0].StartIndex - 1; i < googleResponseList.Queries.Request[0].Count; i++)
                {
                    link = googleResponseList.Items[i].PageMap.Metatags[0].OgUrl;
                    title = googleResponseList.Items[i].PageMap.Metatags[0].OgTitle;
                    desc = googleResponseList.Items[i].PageMap.Metatags[0].OgDesc;

                    if (link.Contains("/in/"))
                    {
                        score += 25;
                    }
                    if (title.ToLower().Contains(firstName))
                    {
                        score += 25;
                    }
                    if (title.ToLower().Contains(lastName))
                    {
                        score += 25;
                    }
                    if (desc.ToLower().Contains(companyName.ToLower()))
                    {
                        score += 25;
                    }

                    if (score >= currentBestScore)
                    {
                        current.Title = title;
                        current.Url = link;
                        current.Desc = desc;
                    }
                    score = 0;
                }

                return current;
            }
            return null;
        }
    }
}