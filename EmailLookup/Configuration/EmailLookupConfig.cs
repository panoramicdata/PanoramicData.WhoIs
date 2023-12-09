using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Core.Configuration;

public class EmailLookupConfig(IServiceCollection services)
{
	internal IServiceCollection Services { get; set; } = services;
}