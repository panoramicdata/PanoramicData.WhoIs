using EmailLookup.Core;

namespace EmailLookup
{
	public class SearchResult
	{
		public enum Outcome
		{
			Failure,
			Success
		}

		public Outcome SearchOutcome { get; set; }
		public Profile Profile { get; set; } = new();
	}
}
