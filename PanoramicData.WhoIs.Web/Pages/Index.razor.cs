using Microsoft.AspNetCore.Components;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Web.Models;

namespace PanoramicData.WhoIs.Web.Pages;

public partial class Index
{
	private bool _showResults;
	private bool _searchDisabled;
	private readonly LookupModel _lookupData = new();
	private string _message = string.Empty;
	private Person _person = new();

	[Inject] protected PersonEnhancer? PersonEnhancer { get; set; }

	[Inject] protected ILogger<Index>? Logger { get; set; }

	private async Task LookupAsync()
	{
		if (PersonEnhancer is null)
		{
			return;
		}

		try
		{
			_searchDisabled = true;
			_showResults = false;
			_person = await PersonEnhancer.EnhanceAsync(new Person
			{
				MailAddress = new(_lookupData.EmailAddress)
			}, default);
			_showResults = true;
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
