using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Configuration
{
	public static class EmailLookupConfigExtensions
	{

		public static EmailLookupConfig AddProxyCurl(this EmailLookupConfig options, string googleCx, string googleKey, string linkedInKey)
		{
			ProxyCurl.ProxyCurlConfig proxyCurlConfig = new();

			proxyCurlConfig.GoogleCx = googleCx;
			proxyCurlConfig.GoogleKey = googleKey;
			proxyCurlConfig.LinkedInKey = linkedInKey;

			// options.Services.AddTransient<IPersonSearcher, GoogleSearcher>();
			options.Services.AddSingleton(ctx => proxyCurlConfig);

			return options;
		}
	}
}
