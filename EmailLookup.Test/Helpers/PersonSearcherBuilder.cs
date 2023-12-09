using EmailLookup.IntegrationTest;
using Microsoft.Extensions.Configuration;

namespace EmailLookup.Test.Helpers;

internal class PersonSearcherBuilder
{
	private readonly IList<Core.IPersonSearcher> _searchers = new List<Core.IPersonSearcher>(10);
	private readonly AppSettings _appSettings;

	public PersonSearcherBuilder()
	{
		var builder = new ConfigurationBuilder();
		builder.AddJsonFile("appsettings.json");
		var configuration = builder.Build();
		_appSettings = configuration
		   .GetSection("AppSettings")
		   .Get<AppSettings>();
	}

	public PersonSearcherBuilder WithProxyCurlSearcher()
	{
		var config = new Core.ProxyCurl.ProxyCurlConfig();
		config.GoogleCx = _appSettings.GoogleCx;
		config.GoogleKey = _appSettings.GoogleKey;
		config.ProxyCurlKey = _appSettings.ProxyCurlKey;

		_searchers.Add(new Core.ProxyCurl.ProxyCurlSearcher(config));
		return this;
	}

	public PersonSearcherBuilder WithWhoIsSearcher()
	{
		_searchers.Add(new Core.WhoIs.WhoIsSearcher());
		return this;
	}

	public Core.PersonSearcher Build() => new(_searchers);
}
