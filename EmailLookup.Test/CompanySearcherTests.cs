using FluentAssertions;

namespace EmailLookup.Test;

public class CompanySearcherTests : TestBase
{
	private readonly CompanySearcher _defaultSearcher = new();
	private readonly CompanySearcher _suppliedWordsSearcher = new([
		"am",
		"bar",
		"or",
		"pan",
		"Panoramic",
		"Panoramic/M",
		"data"
	]);

	[Theory]
	[InlineData("panoramicdata", "Panoramic Data")]
	[InlineData("startingot", "Start Ingot")]
	public async void CompanySearcher_WithFindableCompany_Succeeds(string fqdnFirstPart, string companyName)
	{
		(await _defaultSearcher.GetCompanyNameFromDomainAsync(fqdnFirstPart, default)).Should().Be(companyName);
		(_suppliedWordsSearcher.GetCompanyNameFromDomainAsync(fqdnFirstPart, default)).Should().Be(companyName);
	}
}