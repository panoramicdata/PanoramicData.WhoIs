using EmailLookup.Core.ProxyCurl;
using Whois;

namespace EmailLookup.Core.WhoIs
{
	public class WhoIsSearcher : IPersonSearcher
	{
		private readonly WhoIsConfig _config;

		public WhoIsSearcher(WhoIsConfig config)
		{
			_config = config;
		}

		public async Task<Profile?> SearchAsync(Person person)
		{
			var domain = person.Domain;

			var response = await new WhoisLookup()
				.LookupAsync(domain)
				.ConfigureAwait(false);

			Profile profile = response.ToProfile();

			return profile;
		}
	}
}
