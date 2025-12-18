using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Configuration;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;
using PanoramicData.WhoIs.Interfaces;

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
}
