using Microsoft.Extensions.DependencyInjection;

namespace PanoramicData.WhoIs.Configuration;

public class WhoIsOptions(IServiceCollection services)
{
	internal IServiceCollection Services { get; set; } = services;
}