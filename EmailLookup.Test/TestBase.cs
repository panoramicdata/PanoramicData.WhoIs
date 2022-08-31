using EmailLookup.Core.ProxyCurl;
using EmailLookup.Test.Helpers;
using Microsoft.Extensions.Configuration;

namespace EmailLookup.Test;

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

		//EmailLookup = new Core.EmailLookup(
		//   appSettings.GoogleCx,
		//   appSettings.GoogleKey,
		//   appSettings.LinkedInKey
		//);

		EmailLookup = new EmailLookupBuilder()
			.WithProxyCurlSearcher()
			.WithWhoIsSearcher()
			.Build();

	}

	protected LinkedInSearcher LinkedInSearcher { get; }
	protected Core.EmailLookup EmailLookup { get; }
}