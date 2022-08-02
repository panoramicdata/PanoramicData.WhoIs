using EmailLookup.ProxyCurl.Google;
using Whois;

namespace EmailLookup
{
	public class EmailLookupResult
	{
		public GoogleSearchResponse? Google { get; set; }
		public WhoisResponse? WhoIs { get; set; }

		public DetailedPersonInformation? LinkedIn { get; set; }
	}
}