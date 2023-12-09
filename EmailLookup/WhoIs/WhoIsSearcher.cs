using EmailLookup.Core.ProxyCurl;
using EmailLookup.ProfileResult;
using Whois;

namespace EmailLookup.Core.WhoIs;

/// <summary>
/// An interface that interacts with the WHOIS Lookup API, which returns detailed information
/// on a company's domain information.
/// </summary>
public class WhoIsSearcher : IPersonSearcher
{
	/// <summary>
	/// Searches the WHOIS database for information on a domain and stores that information in a
	/// Profile object, which is then returned.
	/// </summary>
	public async Task<Profile> SearchAsync(Person person)
	{
		var domain = person.Domain;

		var response = await new WhoisLookup()
			.LookupAsync(domain)
			.ConfigureAwait(false);

		Profile profile;
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
