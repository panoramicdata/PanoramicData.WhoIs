using FluentAssertions;

namespace EmailLookup.Test;

public class CompanySearcherTests : TestBase
{
	private List<string> Words { get; set; } = [
		"am",
		"bar",
		"or",
		"pan",
		"Panoramic",
		"data"
	];

	[Fact]
	public void CompanySearcher_WithFindableCompany_Succeeds()
	{
		var searcher = new CompanySearcher(Words);

		var result = searcher.GetCompanyNameFromDomain("panoramicdata");
		result.Should().Be("Panoramic Data");
	}
}