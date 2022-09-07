using EmailLookup.Core.ProxyCurl;
using EmailLookup.Core.WhoIs;

namespace EmailLookup.Core
{
	public class PersonSearcher : IDisposable
	{
		private bool _disposedValue;
		private readonly IEnumerable<IPersonSearcher> _searchers;

		public PersonSearcher(IEnumerable<IPersonSearcher> searchers)
		{
			_searchers = searchers;
		}

		public async Task<Profile> LookupProfileAsync(
		   string mailAddress
		   )
		{
			var person = new Person(mailAddress);
			IList<Profile> profiles = new List<Profile>();
			Profile finalProfile = new();
			Profile? currentProfile = null;

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
