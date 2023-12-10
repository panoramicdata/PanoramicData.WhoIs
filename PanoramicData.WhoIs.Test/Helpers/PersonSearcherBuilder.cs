using Microsoft.Extensions.Configuration;
using PanoramicData.HumanWhoIs.IntegrationTest;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.WhoIs.Test.Helpers;

internal class PersonSearcherBuilder
{
	private readonly List<IPersonEnhancer> _searchers = new(10);
	private readonly AppSettings _appSettings;

	public PersonSearcherBuilder()
	{
		var builder = new ConfigurationBuilder();
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		_appSettings = configuration
		   .GetSection("AppSettings")
		   .Get<AppSettings>() ?? throw new Exception("Unable to deserialize Appsettings.");
	}

	public PersonSearcherBuilder WithProxyCurlSearcher()
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

	public PersonSearcherBuilder WithWhoIsSearcher()
	{
		_searchers.Add(new WhoIsPersonEnhancer());
		return this;
	}

	public PersonEnhancer Build()
		=> new(_searchers);
}
