using EmailLookup.ProxyCurl;
using Whois;

namespace EmailLookup
{
	public class WhoIsSearcher : IPersonSearcher
	{
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
