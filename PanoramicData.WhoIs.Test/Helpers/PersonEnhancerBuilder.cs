using Microsoft.Extensions.Configuration;
using PanoramicData.HumanWhoIs.IntegrationTest;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.WhoIs.Test.Helpers;

internal class PersonEnhancerBuilder
{
	private readonly List<IPersonEnhancer> _searchers = new(10);
	private readonly AppSettings _appSettings;

	public PersonEnhancerBuilder()
	{
		var builder = new ConfigurationBuilder();
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		_appSettings = configuration
		   .GetSection("AppSettings")
		   .Get<AppSettings>() ?? throw new Exception("Unable to deserialize Appsettings.");
	}

	public PersonEnhancerBuilder WithProxyCurlSearcher()
	{
		var config = new ProxyCurlConfig
		{
			GoogleCx = _appSettings.GoogleCx,
			GoogleKey = _appSettings.GoogleKey,
			ProxyCurlKey = _appSettings.ProxyCurlKey
		};

		_searchers.Add(new ProxyCurlPersonEnhancer(config));
		return this;
	}

	public PersonEnhancerBuilder WithWhoIsSearcher()
	{
		_searchers.Add(new WhoIsPersonEnhancer());
		return this;
	}

	public PersonEnhancer Build()
		=> new(_searchers);
}
