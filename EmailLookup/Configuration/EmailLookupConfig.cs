using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Configuration
{
	public class EmailLookupConfig
	{
		internal EmailLookupConfig(IServiceCollection services)
		{
			Services = services;
		}

		internal IServiceCollection Services { get; set; }
	}
}