using PanoramicData.WhoIs.Enhancers;

namespace PanoramicData.WhoIs.IntegrationTest;

public class WhoIsSearcherTests : TestBase
{
	[Fact]
	public async Task ValidDomainSearch_ShouldReturnWhoIsResponse()
	{
		var company = await new WhoIsCompanyEnhancer()
			.EnhanceAsync(new Company
			{
				DomainName = "panoramicdata.com"
			}, CancellationToken);

		company.Should().NotBeNull();
		company.DomainName.Should().Be("panoramicdata.com");
		company.DomainStatus.Should().Be("clientTransferProhibited");
		company.Registrar.Should().NotBeNullOrWhiteSpace();
	}
}