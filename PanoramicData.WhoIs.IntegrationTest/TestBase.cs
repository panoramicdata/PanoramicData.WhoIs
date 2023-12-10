using Microsoft.Extensions.Configuration;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.IntegrationTest.Helpers;
using PanoramicData.WhoIs.ProxyCurl;
using System.Net.Mail;

namespace PanoramicData.HumanWhoIs.IntegrationTest;

public abstract class TestBase
{
	protected ProxyCurlPersonEnhancer ProxyCurlPersonEnhancer { get; }

	protected PersonEnhancer PersonEnhancer { get; }

	protected MailAddress ValidMailAddress { get; }

	protected string ValidFirstName { get; }

	protected string ValidProfileUrl { get; }

	protected TestBase()
	{
		var currentDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
		var rootDirectoryInfo = (currentDirectoryInfo.Parent?.Parent?.Parent)
			?? throw new InvalidOperationException("Failed to identify root directory for this project!");

		// Load AppSettings
		var builder = new ConfigurationBuilder();
		builder.SetBasePath(rootDirectoryInfo.FullName);
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		var appSettings = configuration
			.GetSection("AppSettings")
			.Get<AppSettings>() ?? throw new Exception("Failed to load appsettings.json");

		ValidMailAddress = new(appSettings.ValidEmailAddress);
		ValidFirstName = appSettings.ValidFirstName;
		ValidProfileUrl = appSettings.ValidProfileUrl;

		ProxyCurlPersonEnhancer = new ProxyCurlPersonEnhancer(new ProxyCurlConfig
		{
			GoogleCx = appSettings.GoogleCx,
			GoogleKey = appSettings.GoogleKey,
			ProxyCurlKey = appSettings.ProxyCurlKey,
			ProxyCurlCacheFolder = appSettings.ProxyCurlCacheFolder
		});

		PersonEnhancer = new PersonEnhancerBuilder()
			.WithProxyCurlSearcher()
			.WithWhoIsSearcher()
			.Build();
	}
}