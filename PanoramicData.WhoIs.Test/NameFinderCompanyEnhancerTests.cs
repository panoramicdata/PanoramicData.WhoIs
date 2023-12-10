using FluentAssertions;

namespace PanoramicData.WhoIs.Test;

public class NameFinderCompanyEnhancerTests : TestBase
{
	private readonly NameFinderCompanyEnhancer _defaultEnhancer = new();
	private readonly NameFinderCompanyEnhancer _suppliedWordsEnhancer = new([
		"am",
		"bar",
		"or",
		"pan",
		"Panoramic",
		"Panoramic/M",
		"data",
		"starting",
		"start",
		"ingot"
	]);

	[Theory]
	[InlineData("panoramicdata.com", "Panoramic Data")]
	[InlineData("startingot.co.uk", "Start Ingot")]
	public async void CompanyEnhancer_WithFindableCompany_Succeeds(string domainName, string companyName)
	{
		var company = new Company
		{
			DomainName = domainName
		};
		var defaultEnhancedCompany = await _defaultEnhancer.EnhanceAsync(company, default);
		defaultEnhancedCompany.Name.Should().Be(companyName);

		var suppliedWordsEnhancedCompany = await _suppliedWordsEnhancer.EnhanceAsync(company, default);
		suppliedWordsEnhancedCompany.Name.Should().Be(companyName);
	}
}