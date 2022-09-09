using EmailLookup.Core;
using EmailLookup.Demo.Web.Models;
using Microsoft.AspNetCore.Components;
using cre = EmailLookup.Core;

namespace EmailLookup.Demo.Web.Pages
{
	public partial class Index
	{
		private LookupModel _lookupData = new LookupModel();
		private string _message = string.Empty;

		[Inject] protected cre.PersonSearcher? Searcher { get; set; }

		[Inject] protected ILogger<Index>? Logger { get; set; }

		private async Task LookupAsync()
		{
			if (Searcher is null) return;

			try
			{
				var result = await Searcher.LookupProfileAsync(_lookupData.EmailAddress);
				_message = "Hello, " + result.FirstName + " " + result.LastName + "!";
			}
			catch (Exception ex)
			{
				if (Logger is not null)
				{
					Logger.LogError(ex, "Lookup failed! Message: {Message}", ex.Message);
				}
				_message = "Lookup failed. See log for details.";
			}
		}

		private void PrintResults(Profile? result)
		{

		}
	}
}
