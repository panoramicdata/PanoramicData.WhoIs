using Whois;

namespace EmailLookup
{
   public class EmailLookupResult
   {
	  public LinkedinGoogleSearchResponse? Google { get; set; }
	  public WhoisResponse? WhoIs { get; set; }
   }
}