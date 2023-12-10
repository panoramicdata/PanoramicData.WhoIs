using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Configuration;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.WhoIs.Extensions;

public static class WhoIsOptionsExtensions
{

	public static WhoIsOptions AddProxyCurl(this WhoIsOptions options, string googleCx, string googleKey, string proxyCurlKey)
	{
		ProxyCurlConfig proxyCurlConfig = new()
		{
			GoogleCx = googleCx,
			GoogleKey = googleKey,
			ProxyCurlKey = proxyCurlKey
		};

		options.Services.AddTransient<IPersonEnhancer, ProxyCurlPersonEnhancer>();
		options.Services.AddSingleton(ctx => proxyCurlConfig);

		return options;
	}

	public static WhoIsOptions AddWhoIs(this WhoIsOptions options)
	{
		options.Services.AddTransient<IPersonEnhancer, WhoIsPersonEnhancer>();
		return options;
	}
}
