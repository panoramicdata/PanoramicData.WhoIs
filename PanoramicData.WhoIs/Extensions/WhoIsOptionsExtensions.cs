using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Configuration;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Extensions;

/// <summary>
/// Extension methods for configuring optional WhoIs enrichment providers on <see cref="WhoIsOptions"/>.
/// </summary>
public static class WhoIsOptionsExtensions
{

	/// <summary>
	/// Registers the ProxyCurl person enhancer and its Google Custom Search dependencies
	/// with the dependency injection container.
	/// </summary>
	/// <param name="options">The <see cref="WhoIsOptions"/> instance to configure.</param>
	/// <param name="googleCx">The Google Custom Search Engine ID (cx parameter).</param>
	/// <param name="googleKey">The Google API key for Custom Search requests.</param>
	/// <param name="proxyCurlKey">The API key for the ProxyCurl LinkedIn enrichment service.</param>
	/// <returns>The same <paramref name="options"/> instance, for chaining.</returns>
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
