using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using Whois;

namespace EmailLookup
{
   public class Program
   {
	  public static async Task Main()
	  {
		 var configuration = new ConfigurationBuilder()
			 .AddJsonFile("appsettings.json", optional: true)
			 .Build();
		 var googleCx = configuration["AppSettings:GoogleCx"];
		 var googleKey = configuration["AppSettings:GooglKey"];

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

		 Console.WriteLine($"Domain is {mailAddress.Host}");

		 var whoIsResponse = await new WhoisLookup()
			.LookupAsync(mailAddress.Host)
			.ConfigureAwait(false);
		 Console.WriteLine($"Whois: {whoIsResponse.Content}");


		 var googleSearchResponse = await new GoogleSearcher(googleCx, googleKey)
			.SearchLinkedInAsync(new Person(mailAddress))
			.ConfigureAwait(false);

		 if (googleSearchResponse is null)
		 {
			Console.WriteLine("No results found.");
		 }
		 else
		 {
			Console.WriteLine($"Google search: {googleSearchResponse.Description}");
		 }
	  }
   }
}