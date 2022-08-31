using EmailLookup.Core.ProxyCurl;
using EmailLookup.Core.ProxyCurl.Google;
using Whois;

namespace EmailLookup.Core
{
	public class EmailLookupResult
	{
		public WhoisResponse? WhoIs { get; set; }
		public Profile LinkedIn { get; set; }
	}
}