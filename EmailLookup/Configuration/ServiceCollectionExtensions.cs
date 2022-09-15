using EmailLookup.Core.Configuration;

namespace Microsoft.Extensions.DependencyInjection
{
	public static class ServiceCollectionExtensions
	{
		public static IServiceCollection AddEmailLookup(this IServiceCollection services, Action<EmailLookupConfig> options)
		{
			var settings = new EmailLookupConfig(services);

			options.Invoke(settings);

			services.AddSingleton(settings);
			services.AddTransient<EmailLookup.Core.PersonSearcher>();

			return services;
		}
	}
}
