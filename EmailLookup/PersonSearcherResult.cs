using EmailLookup.Core.ProxyCurl;
using EmailLookup.Core.ProxyCurl.Google;
using Whois;

namespace EmailLookup.Core
{
	public class PersonSearcherResult
	{
		public WhoisResponse? WhoIs { get; set; }
		public Profile? ProxyCurl { get; set; }
	}
}