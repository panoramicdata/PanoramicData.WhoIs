using EmailLookup.Core.ProxyCurl;
using EmailLookup.IntegrationTest.Helpers;
using Microsoft.Extensions.Configuration;

namespace EmailLookup.IntegrationTest;

public abstract class TestBase
{
	protected ProxyCurlSearcher ProxyCurlSearcher { get; }
	protected Core.PersonSearcher PersonSearcher { get; }
	protected string ValidEmailAddress { get; } = string.Empty;
	protected string ValidFirstname { get; } = string.Empty;
	protected string ValidProfileUrl { get; } = string.Empty;

	public TestBase()
	{
		// Load appsettings
		var builder = new ConfigurationBuilder();
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		var appSettings = configuration
		   .GetSection("AppSettings")
		   .Get<AppSettings>();

		ValidEmailAddress = appSettings.ValidEmailAddress;
		ValidFirstname = appSettings.ValidFirstname;
		ValidProfileUrl = appSettings.ValidProfileUrl;

		ProxyCurlSearcher = new ProxyCurlSearcher(new ProxyCurlConfig
		{
			GoogleCx = appSettings.GoogleCx,
			GoogleKey = appSettings.GoogleKey,
			ProxyCurlKey = appSettings.ProxyCurlKey
		});

		PersonSearcher = new PersonSearcherBuilder()
			.WithProxyCurlSearcher()
			.WithWhoIsSearcher()
			.Build();

	}

}