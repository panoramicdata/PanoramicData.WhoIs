using System.Net.Mail;

namespace EmailLookup
{
   public class EmailLookup : IDisposable
   {
	  private bool disposedValue;
	  private readonly GoogleSearcher _googleSearcher;
	  private readonly WhoIsSearcher _whoIs;

	  public EmailLookup(string googleCx, string googleKey)
	  {
		 _googleSearcher = new GoogleSearcher(googleCx, googleKey);
		 _whoIs = new WhoIsSearcher();
	  }

	  public async Task<EmailLookupResult> LookupAsync(
		 MailAddress mailAddress,
		 CancellationToken cancellationToken
		 )
	  {
		 var person = new Person(mailAddress);
		 return new EmailLookupResult
		 {
			Google = await _googleSearcher.SearchLinkedInAsync(person, cancellationToken).ConfigureAwait(false),
			WhoIs = await _whoIs.GetResponseAsync(person.Domain, cancellationToken).ConfigureAwait(false)
		 };
	  }

	  protected virtual void Dispose(bool disposing)
	  {
		 if (!disposedValue)
		 {
			if (disposing)
			{
			   // TODO: dispose managed state (managed objects)
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			disposedValue = true;
		 }
	  }

	  // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
	  // ~EmailLookup()
	  // {
	  //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
	  //     Dispose(disposing: false);
	  // }

	  public void Dispose()
	  {
		 // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		 Dispose(disposing: true);
		 GC.SuppressFinalize(this);
	  }
   }
}
