using EmailLookup.Demo.Web.Models;
using EmailLookup.ProfileResult;
using Microsoft.AspNetCore.Components;
using cre = EmailLookup.Core;

namespace EmailLookup.Demo.Web.Pages
{
	public partial class Index
	{
		private bool _showResults;
		private bool _searchDisabled;
		private readonly LookupModel _lookupData = new LookupModel();
		private string _message = string.Empty;
		private Profile _searchResults = new Profile();
		private SearchResult _result = new SearchResult();

		[Inject] protected cre.PersonSearcher? Searcher { get; set; }

		[Inject] protected ILogger<Index>? Logger { get; set; }

		private async Task LookupAsync()
		{
			if (Searcher is null)
			{
				return;
			}

			try
			{
				_searchDisabled = true;
				_showResults = false;
				_result = await Searcher.LookupProfileAsync(_lookupData.EmailAddress);
				if (_result.SearchOutcome == SearchResult.Outcome.Failure)
				{
					_showResults = false;
				}
				else
				{
					_showResults = true;
					_searchResults = _result.Profile;
				}
			}
			catch (Exception ex)
			{
				if (Logger is not null)
				{
					Logger.LogError(ex, "Lookup failed! Message: {Message}", ex.Message);
				}

				_message = ("Lookup failed! " + ex.Message);
			}
			finally
			{
				_searchDisabled = false;
			}
		}
	}
}
