using EmailLookup.Core.ProxyCurl;
using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Core.Configuration
{
	public static class EmailLookupConfigExtensions
	{

		public static EmailLookupConfig AddProxyCurl(this EmailLookupConfig options, string googleCx, string googleKey, string linkedInKey)
		{
			ProxyCurl.ProxyCurlConfig proxyCurlConfig = new();

			proxyCurlConfig.GoogleCx = googleCx;
			proxyCurlConfig.GoogleKey = googleKey;
			proxyCurlConfig.LinkedInKey = linkedInKey;

			options.Services.AddTransient<IPersonSearcher, LinkedInSearcher>();
			options.Services.AddSingleton(ctx => proxyCurlConfig);

			return options;
		}

		public static EmailLookupConfig AddWhoIs(this EmailLookupConfig options)
		{
			WhoIs.WhoIsConfig whoisConfig = new();

			options.Services.AddTransient<IPersonSearcher, WhoIs.WhoIsSearcher>();
			options.Services.AddSingleton(ctx => whoisConfig);

			return options;
		}
	}
}
