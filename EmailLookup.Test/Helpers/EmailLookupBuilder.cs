using Microsoft.Extensions.Configuration;

namespace EmailLookup.Test.Helpers
{
	internal class EmailLookupBuilder
	{
		private readonly IList<Core.IPersonSearcher> _searchers = new List<Core.IPersonSearcher>(10);
		private readonly AppSettings _appSettings;

		public EmailLookupBuilder()
		{
			var builder = new ConfigurationBuilder();
			builder.AddJsonFile("appsettings.json");
			var configuration = builder.Build();
			_appSettings = configuration
			   .GetSection("AppSettings")
			   .Get<AppSettings>();
		}

		public EmailLookupBuilder WithProxyCurlSearcher()
		{
			var config = new Core.ProxyCurl.ProxyCurlConfig();
			config.GoogleCx = _appSettings.GoogleCx;
			config.GoogleKey = _appSettings.GoogleKey;
			config.LinkedInKey = _appSettings.LinkedInKey;

			_searchers.Add(new Core.ProxyCurl.LinkedInSearcher(config));
			return this;
		}

		public EmailLookupBuilder WithWhoIsSearcher()
		{
			_searchers.Add(new Core.WhoIs.WhoIsSearcher(new Core.WhoIs.WhoIsConfig()));
			return this;
		}

		public EmailLookup.Core.EmailLookup Build()
		{
			return new Core.EmailLookup(_searchers);
		}
	}
}
