using Microsoft.Extensions.Configuration;

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

		 GoogleSearcher = new GoogleSearcher(
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

	  protected GoogleSearcher GoogleSearcher { get; }
	  protected EmailLookup EmailLookup { get; }
   }
}