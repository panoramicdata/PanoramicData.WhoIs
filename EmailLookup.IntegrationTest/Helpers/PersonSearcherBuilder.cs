using Microsoft.Extensions.Configuration;

namespace EmailLookup.IntegrationTest.Helpers
{
	internal class PersonSearcherBuilder
	{
		private readonly IList<Core.IPersonSearcher> _searchers = new List<Core.IPersonSearcher>(10);
		private readonly AppSettings _appSettings;

		public PersonSearcherBuilder()
		{
			var currentDirectoryInfo = new DirectoryInfo(Directory.GetCurrentDirectory());
			var rootDirectoryInfo = currentDirectoryInfo.Parent?.Parent?.Parent;
			if (rootDirectoryInfo is null)
			{
				throw new InvalidOperationException("Failed to identify root directory for this project!");
			}

			var builder = new ConfigurationBuilder();
			builder.SetBasePath(rootDirectoryInfo.FullName);
			builder.AddJsonFile("appsettings.json");
			var configuration = builder.Build();
			_appSettings = configuration
			   .GetSection("AppSettings")
			   .Get<AppSettings>();
		}

		public PersonSearcherBuilder WithProxyCurlSearcher()
		{
			var config = new Core.ProxyCurl.ProxyCurlConfig
			{
				GoogleCx = _appSettings.GoogleCx,
				GoogleKey = _appSettings.GoogleKey,
				ProxyCurlKey = _appSettings.ProxyCurlKey
			};

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
}
