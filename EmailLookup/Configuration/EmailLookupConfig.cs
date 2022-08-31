using Microsoft.Extensions.DependencyInjection;

namespace EmailLookup.Core.Configuration
{
	public class EmailLookupConfig
	{
		public EmailLookupConfig(IServiceCollection services)
		{
			Services = services;
		}

		internal IServiceCollection Services { get; set; }
	}
}