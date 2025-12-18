using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Enhancers;

namespace PanoramicData.WhoIs.Configuration;

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
