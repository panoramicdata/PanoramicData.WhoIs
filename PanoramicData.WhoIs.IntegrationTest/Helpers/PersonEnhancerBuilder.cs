using Microsoft.Extensions.Configuration;
using PanoramicData.HumanWhoIs.IntegrationTest;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.WhoIs.IntegrationTest.Helpers;

internal class PersonEnhancerBuilder
{
	private readonly List<IPersonEnhancer> _personEnhancers = new(10);
	private readonly AppSettings _appSettings;

	public PersonEnhancerBuilder()
	{
		var currentDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
		var rootDirectoryInfo = (currentDirectoryInfo.Parent?.Parent?.Parent) ?? throw new InvalidOperationException("Failed to identify root directory for this project!");
		var builder = new ConfigurationBuilder();
		builder.SetBasePath(rootDirectoryInfo.FullName);
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		_appSettings = configuration
		   .GetSection("AppSettings")
		   .Get<AppSettings>() ?? throw new Exception("Failed to load appsettings.json");
	}

	public PersonEnhancerBuilder WithProxyCurlEnhancer()
	{
		var config = new ProxyCurlConfig
		{
			GoogleCx = _appSettings.GoogleCx,
			GoogleKey = _appSettings.GoogleKey,
			ProxyCurlKey = _appSettings.ProxyCurlKey
		};

		_personEnhancers.Add(new ProxyCurlPersonEnhancer(config));
		return this;
	}

	public PersonEnhancer Build() => new(_personEnhancers);
}
