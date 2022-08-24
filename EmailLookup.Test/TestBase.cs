using Microsoft.Extensions.Configuration;
using EmailLookup.ProxyCurl;

namespace EmailLookup.Test
{
   public abstract class TestBase
   {
	  public TestBase()
	  {
		 // Load appsettings
		 var builder = new ConfigurationBuilder();
		 builder.AddJsonFile("appsettings.json");
		 var configuration = builder.Build();
		 var appSettings = configuration
			.GetSection("AppSettings")
			.Get<AppSettings>();

		 LinkedInSearcher = new LinkedInSearcher(
			appSettings.GoogleCx,
			appSettings.GoogleKey,
			appSettings.LinkedInKey
		);

		 EmailLookup = new EmailLookup(
			appSettings.GoogleCx,
			appSettings.GoogleKey,
            appSettings.LinkedInKey
		 );;
	  }	

	  protected LinkedInSearcher LinkedInSearcher { get; }
	  protected EmailLookup EmailLookup { get; }
   }
}