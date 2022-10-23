using EmailLookup.ProfileResult;
using EmailLookup.CustomExceptions;

namespace EmailLookup.Core
{
	/// <summary>
	/// The <c>PersonSearcher</c> class is created by the user to
	/// perform the EmailLookup
	/// </summary>
	/// <remarks>
	/// <para>This is the only class that users will interact
	/// with - it's responsible for creating every other object
	/// used in the EmailLookup process, and determines the order
	/// in which the program executes.</para>
	/// </remarks>
	public class PersonSearcher : IDisposable
	{
		private bool _disposedValue;
		private readonly IEnumerable<Core.IPersonSearcher> _searchers;

		/// <summary>
		/// Initializes a new instance of PersonSearcher and
		/// assigns the given searcher list to an internal variable.
		/// </summary>
		/// <param name="searchers">A list of objects that use the
		/// IPersonSearcher interface.</param>
		/// <remarks>
		/// <para>
		/// Users can choose which searchers to include in their lookup
		/// by passing them into the searchers parameter.
		/// </para></remarks>
		public PersonSearcher(IEnumerable<Core.IPersonSearcher> searchers)
		{
			_searchers = searchers;
		}

		/// <summary>
		/// Takes an email address, passes it to the list of
		/// searchers and returns the resulting profile.
		/// </summary>
		/// <param name="mailAddress">The email address the user wants to lookup.</param>
		/// <returns>A SearchResult object containing information on the inputted email address.</returns>
		public async Task<SearchResult> LookupProfileAsync(
		   string mailAddress
		   )
		{
			Person person;
			// create person object from mail address to pass as parameter in searchers
			if (CheckEmailValidity(mailAddress))
			{
				person = new Person(mailAddress);
			}
			else
			{
				throw new InvalidEmailException(mailAddress);
			}
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

		public static bool CheckEmailValidity(string address)
		{
			try
			{
				var emailAddress = new System.Net.Mail.MailAddress(address.Trim());
				return emailAddress.Address == address.Trim();
			}
			catch
			{
				return false;
			}
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
