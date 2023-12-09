using EmailLookup.ProfileResult;
using Whois;

namespace EmailLookup.Core;

public class PersonSearcherResult
{
	public WhoisResponse? WhoIs { get; set; }
	public Profile? ProxyCurl { get; set; }
}