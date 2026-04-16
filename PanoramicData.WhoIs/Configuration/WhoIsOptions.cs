using Microsoft.Extensions.DependencyInjection;

namespace PanoramicData.WhoIs.Configuration;

/// <summary>
/// Configuration options for the WhoIs services, providing access to the
/// dependency injection service collection for registering additional enhancers.
/// </summary>
/// <param name="services">The dependency injection service collection.</param>
public class WhoIsOptions(IServiceCollection services)
{
	internal IServiceCollection Services { get; set; } = services;
}