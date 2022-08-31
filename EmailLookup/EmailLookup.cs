﻿using EmailLookup.ProxyCurl.Google;
using EmailLookup.ProxyCurl;
using System.Collections;

namespace EmailLookup
{
	public class EmailLookup : IDisposable
	{
		private bool disposedValue;
		private readonly LinkedInSearcher _linkedInSearcher;
		private readonly WhoIsSearcher _whoIs;
		private readonly IEnumerable<IPersonSearcher> _searchers;

		public EmailLookup(string googleCx, string googleKey, string linkedInKey)
		{
			_linkedInSearcher = new LinkedInSearcher(googleCx, googleKey, linkedInKey);
			_whoIs = new WhoIsSearcher();
		}

		//public EmailLookup(IEnumerable<IPersonSearcher> searchers)
		//{
		//	_searchers = searchers;
		//}

		public async Task<Profile> LookupProfileAsync(
		   string mailAddress,
		   CancellationToken cancellationToken
		   )
		{
			var person = new Person(mailAddress);
			IList<Profile> profiles = new List<Profile>();
			Profile finalProfile = new();
			Profile? currentProfile = new();

			IPersonSearcher[] searchers = new IPersonSearcher[]
			{
				_linkedInSearcher,
				_whoIs
			};

			Searchers _searchers = new Searchers(searchers);

			foreach (var searcher in _searchers)
			{
				currentProfile = await searcher.SearchAsync(person);
				if (currentProfile is not null)
				{
					profiles.Add(currentProfile);
				}
			}

			foreach (var profile in profiles)
			{
				ProfileMerger.Merge(profile, finalProfile);
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
