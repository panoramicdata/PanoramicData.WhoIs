using EmailLookup.Demo.Web.Models;
using Microsoft.AspNetCore.Components;
using cre = EmailLookup.Core;

namespace EmailLookup.Demo.Web.Pages
{
	public partial class Index
	{
		private LookupModel _lookupData = new LookupModel();
		private string _message = string.Empty;

		[Inject] private cre.EmailLookup? Searcher { get; set; }

		private async Task LookupAsync()
		{
			if (Searcher is null) return;

			await Searcher.LookupProfileAsync(_lookupData.EmailAddress, CancellationToken.None);
			_message = "Lookup complete!";
		}
	}
}
