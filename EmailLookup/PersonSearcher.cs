﻿namespace EmailLookup.Core
{
	public class PersonSearcher : IDisposable
	{
		private bool _disposedValue;
		private readonly IList<Core.IPersonSearcher> _searchers = new List<Core.IPersonSearcher>(10);

		public PersonSearcher(IList<Core.IPersonSearcher> searchers)
		{
			_searchers = searchers;
		}

		// temporary constructor while configuration is being figured out
		public PersonSearcher(string googleCx, string googleKey, string proxyCurlKey)
		{
			var config = new Core.ProxyCurl.ProxyCurlConfig
			{
				GoogleCx = googleCx,
				GoogleKey = googleKey,
				ProxyCurlKey = proxyCurlKey
			};
			_searchers.Add(new Core.ProxyCurl.ProxyCurlSearcher(config));
			_searchers.Add(new Core.WhoIs.WhoIsSearcher());
		}

		public async Task<SearchResult> LookupProfileAsync(
		   string mailAddress
		   )
		{
			// create person object from mail address to pass as parameter in searchers
			var person = new Person(mailAddress);
			IList<Profile> profiles = new List<Profile>();
			Profile finalProfile = new();

			// for every object that uses IPersonSearcher
			foreach (var searcher in _searchers)
			{
				// perform the search function
				Profile currentProfile = await searcher.SearchAsync(person);
					// and add that to the list of profiles to be merged
					profiles.Add(currentProfile);
			}

			// merge every profile obtaining from searchers
			foreach (var profile in profiles)
			{
				ProfileMerger.Merge(profile, finalProfile);
			}
			var result = new SearchResult()
			{
				SearchOutcome = SearchResult.Outcome.Failure
			};
			if (finalProfile.Outcome == LookupOutcomes.Found)
			{
				result.SearchOutcome = SearchResult.Outcome.Success;
				result.Profile = finalProfile;
			}
			return result;
		}

		protected virtual void Dispose(bool disposing)
		{
			if (!_disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects)
				}

				// TODO: free unmanaged resources (unmanaged objects) and override finalizer
				// TODO: set large fields to null
				_disposedValue = true;
			}
		}

		// // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
		// ~PersonSearcher()
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
