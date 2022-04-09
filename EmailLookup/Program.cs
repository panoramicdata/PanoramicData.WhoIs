using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Whois;

namespace EmailLookup
{
    public class Program
    {
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

            String nameQuery = firstName + "%20" + lastName + "%20" + mailAddress.Host.Substring(0, mailAddress.Host.IndexOf("."));

            //Console.WriteLine(whoIsResponse.Content);

            //var linkedInApi = new LinkedInApi(linkedinConfig);
            //var scope = AuthorizationScope.ReadBasicProfile | AuthorizationScope.ReadEmailAddress;
            //var state = Guid.NewGuid().ToString();
            //var redirectUrl = "http://mywebsite/LinkedIn/OAuth2";
            //var url = linkedInApi.OAuth2.GetAuthorizationUrl(scope, state, redirectUrl);
            //Console.WriteLine(url);

            var googleCx = configuration["AppSettings:GOOGLE_CX"];
            var googleKey = configuration["AppSettings:GOOGLE_KEY"];

            var googleApiUrl =
                "https://customsearch.googleapis.com/customsearch/v1?cx=" + googleCx + "&q=" + nameQuery + "&key="  + googleKey;
            var response = http.Request(googleApiUrl);
        }
    }
}