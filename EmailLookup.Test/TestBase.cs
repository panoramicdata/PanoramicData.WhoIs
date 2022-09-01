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

		ProxyCurlSearcher = new ProxyCurlSearcher(
		   appSettings.GoogleCx,
		   appSettings.GoogleKey,
		   appSettings.ProxyCurlKey
		);

		//PersonSearcher = new Core.PersonSearcher(
		//   appSettings.GoogleCx,
		//   appSettings.GoogleKey,
		//   appSettings.ProxyCurlKey
		//);

		PersonSearcher = new PersonSearcherBuilder()
			.WithProxyCurlSearcher()
			.WithWhoIsSearcher()
			.Build();

	}

	protected ProxyCurlSearcher ProxyCurlSearcher { get; }
	protected Core.PersonSearcher PersonSearcher { get; }
}