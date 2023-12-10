using PanoramicData.WhoIs.Configuration;
using PanoramicData.WhoIs.Enhancers;

namespace Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
	public static IServiceCollection AddWhoIs(this IServiceCollection services, Action<WhoIsOptions> options)
	{
		var settings = new WhoIsOptions(services);

		options.Invoke(settings);

		services.AddSingleton(settings);
		services.AddTransient<PersonEnhancer>();

		return services;
	}
}
