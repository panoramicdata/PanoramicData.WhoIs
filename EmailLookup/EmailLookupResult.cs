using EmailLookup.ProxyCurl;
using EmailLookup.ProxyCurl.Google;
using Whois;

namespace EmailLookup
{
	public class EmailLookupResult
	{
		public WhoisResponse? WhoIs { get; set; }
		public Profile LinkedIn { get; set; }
	}
}