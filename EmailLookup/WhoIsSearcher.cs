using Whois;

namespace EmailLookup
{
   public class WhoIsSearcher
   {
	  public async Task<WhoisResponse?> GetResponseAsync(
		 string domain,
		 CancellationToken cancellationToken
		 )
	  {
		 Console.WriteLine(domain);
		 var response = await new WhoisLookup()
			.LookupAsync(domain)
			.ConfigureAwait(false);

		 return response;
	  }
   }
}
