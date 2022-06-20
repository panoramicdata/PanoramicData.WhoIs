namespace EmailLookup
{
	public class EmailLookup : IDisposable
	{
		private bool disposedValue;
		private readonly GoogleSearcher _googleSearcher;
		private readonly WhoIsSearcher _whoIs;
		private readonly IEnumerable<IPersonSearcher> _searchers;

		public EmailLookup(string googleCx, string googleKey, string linkedInKey)
		{
			_googleSearcher = new GoogleSearcher(googleCx, googleKey, linkedInKey);
			_whoIs = new WhoIsSearcher();
		}

		//public EmailLookup(IEnumerable<IPersonSearcher> searchers)
		//{
		//	_searchers = searchers;
		//}

		public async Task<EmailLookupResult> LookupAsync(
		   string mailAddress,
		   CancellationToken cancellationToken
		   )
		{
			var person = new Person(mailAddress);
			IList<Profile> profiles = new List<Profile>();
			Profile finalProfile = new();

			return new EmailLookupResult
			{
				Google = await _googleSearcher.SearchGoogleAsync(person.Email, cancellationToken).ConfigureAwait(false),
				WhoIs = await _whoIs.GetResponseAsync(person.Domain, cancellationToken).ConfigureAwait(false),
				LinkedIn = await _googleSearcher.SearchLinkedInAsync(person.Email, cancellationToken).ConfigureAwait(false)
			};
		}

		public async Task<Profile> LookupProfileAsync(
		   string mailAddress,
		   CancellationToken cancellationToken
		   )
		{
			var person = new Person(mailAddress);
			IList<Profile> profiles = new List<Profile>();
			Profile finalProfile = new();

			foreach (var searcher in _searchers)
			{
				profiles.Add(await searcher.SearchAsync(person));
			}

			var merger = new ProfileMerger();
			foreach (var profile in profiles)
			{
				merger.Merge(profile, finalProfile);
			}

			return finalProfile;
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
