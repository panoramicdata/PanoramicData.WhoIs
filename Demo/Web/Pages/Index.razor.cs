using EmailLookup.Core;
using EmailLookup.Demo.Web.Models;
using EmailLookup.ProfileResult;
using Microsoft.AspNetCore.Components;

namespace EmailLookup.Demo.Web.Pages;

public partial class Index
{
	private bool _showResults;
	private bool _searchDisabled;
	private readonly LookupModel _lookupData = new();
	private string _message = string.Empty;
	private Profile _searchResults = new();
	private SearchResult _result = new();

	[Inject] protected PersonSearcher? Searcher { get; set; }

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
			Logger?.LogError(ex, "Lookup failed! Message: {Message}", ex.Message);

			_message = ("Lookup failed! " + ex.Message);
		}
		finally
		{
			_searchDisabled = false;
		}
	}
}
