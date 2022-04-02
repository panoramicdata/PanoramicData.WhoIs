using Microsoft.Extensions.Configuration;
using Sparkle.LinkedInNET;
using Whois;

public class Program
{
    static HttpClient client = new HttpClient();

    static Boolean checkEmailValid(String email)
    {
        if (String.IsNullOrWhiteSpace(email))
        {
            return false;
        }

        else if (email.EndsWith("."))
        {
            return false;
        }
        
        else
        {
            try
            {
                var address = new System.Net.Mail.MailAddress(email);
                return address.Address == email;
            }
            catch
            {
                return false;
            }
        }

    }

    static void WhoIsLookup(string domain)
    {
        var whois = new WhoisLookup();

        var response = whois.Lookup(domain);

        Console.WriteLine(response.Content);
    }

    static String[] seperateEmail(String email)
    {
        int atSign = email.IndexOf('@');
        string domain = email.Substring(atSign + 1);
        string name = email.Substring(0, atSign);
        String[] fullEmail = new string[] { domain, name };
        return fullEmail;
    }

    static void Main()
    {
        IConfigurationRoot configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json", optional: true)
            .Build();
        var LIKEY = configuration["AppSettings:LINKEDIN_API_KEY"];
        var LISECRET = configuration["AppSettings:LINKEDIN_API_SECRET"];

        var linkedinConfig = new LinkedInApiConfiguration(LIKEY, LISECRET);
        var linkedinapi = new LinkedInApi(linkedinConfig);
    
        String inputtedEmail = "default";
        Boolean isValid = false;
        while (!isValid)
        {
            Console.WriteLine("Please enter a valid email address.");
            inputtedEmail = Console.ReadLine();
            if (!(String.IsNullOrWhiteSpace(inputtedEmail)))
            {
                inputtedEmail = inputtedEmail.Trim();
                isValid = checkEmailValid(inputtedEmail);
            }
        }
        String[] email = seperateEmail(inputtedEmail);
        
        string domain = email[0];
        string name = email[1];

        WhoIsLookup(domain);

    }
}