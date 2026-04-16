using Microsoft.Extensions.DependencyInjection;
using PanoramicData.WhoIs.Enhancers;

namespace PanoramicData.WhoIs.Configuration;

/// <summary>
/// Extension methods for registering WhoIs services with the dependency injection container.
/// </summary>
public static class ServiceCollectionExtensions
{
	/// <summary>
	/// Registers the WhoIs services with the dependency injection container and applies
	/// the specified configuration options.
	/// </summary>
	/// <param name="services">The service collection to add the WhoIs services to.</param>
	/// <param name="options">A delegate used to configure <see cref="WhoIsOptions"/>.</param>
	/// <returns>The original <paramref name="services"/> collection, for chaining.</returns>
	public static IServiceCollection AddWhoIs(this IServiceCollection services, Action<WhoIsOptions> options)
	{
		var settings = new WhoIsOptions(services);

		options.Invoke(settings);

		services.AddSingleton(settings);
		services.AddTransient<PersonEnhancer>();

		return services;
	}
}
