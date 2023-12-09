using EmailLookup.Core.ProxyCurl;
using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Core.Configuration
{
	public static class EmailLookupConfigExtensions
	{

		public static EmailLookupConfig AddProxyCurl(this EmailLookupConfig options, string googleCx, string googleKey, string proxyCurlKey)
		{
			ProxyCurlConfig proxyCurlConfig = new()
			{
				GoogleCx = googleCx,
				GoogleKey = googleKey,
				ProxyCurlKey = proxyCurlKey
			};

			options.Services.AddTransient<IPersonSearcher, ProxyCurlSearcher>();
			options.Services.AddSingleton(ctx => proxyCurlConfig);

			return options;
		}

		public static EmailLookupConfig AddWhoIs(this EmailLookupConfig options)
		{
			options.Services.AddTransient<IPersonSearcher, WhoIs.WhoIsSearcher>();
			return options;
		}
	}
}
