using Whois;

namespace EmailLookup
{
	public class WhoIsSearcher : IPersonSearcher
	{
		public async Task<Profile?> SearchAsync(Person person)
		{
			CancellationToken cancellationToken = default;
			var domain = person.Domain;

			var response = await new WhoisLookup()
				.LookupAsync(domain)
				.ConfigureAwait(false);

			return null;
		}
		
		public async Task<WhoisResponse?> GetResponseAsync(
		   string domain,
		   CancellationToken cancellationToken
		   )
		{
			var response = await new WhoisLookup()
			   .LookupAsync(domain)
			   .ConfigureAwait(false);

			return response;
		}
	}
}
