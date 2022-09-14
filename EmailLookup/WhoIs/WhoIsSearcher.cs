using EmailLookup.Core.ProxyCurl;
using Whois;

namespace EmailLookup.Core.WhoIs
{
	public class WhoIsSearcher : IPersonSearcher
	{
		public async Task<Profile> SearchAsync(Person person)
		{
			var domain = person.Domain;

			var response = await new WhoisLookup()
				.LookupAsync(domain)
				.ConfigureAwait(false);

			Profile? profile;
			if (response != null)
			{
				profile = response.ToProfile();
				return profile;
			}
			profile = new Profile
			{
				Outcome = LookupOutcomes.NotFound
			};

			return profile;
		}
	}
}
