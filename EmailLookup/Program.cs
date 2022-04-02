using Microsoft.Extensions.Configuration;
using Sparkle.LinkedInNET;
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
            var LIKEY = configuration["AppSettings:LINKEDIN_API_KEY"];
            var LISECRET = configuration["AppSettings:LINKEDIN_API_SECRET"];

            var linkedinConfig = new LinkedInApiConfiguration(LIKEY, LISECRET);

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

            Console.WriteLine(whoIsResponse.Content);

            var linkedinapi = new LinkedInApi(linkedinConfig);
        }
    }
}