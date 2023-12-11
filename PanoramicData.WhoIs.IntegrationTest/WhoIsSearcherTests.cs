using FluentAssertions;
using PanoramicData.WhoIs;
using PanoramicData.WhoIs.Enhancers;

namespace PanoramicData.HumanWhoIs.IntegrationTest;

public class WhoIsSearcherTests : TestBase
{
	[Fact]
	public async Task ValidDomainSearch_ShouldReturnWhoIsResponse()
	{
		var company = await new WhoIsCompanyEnhancer()
			.EnhanceAsync(new Company
			{
				DomainName = "panoramicdata.com"
			}, default)
			.ConfigureAwait(false);

		company.Should().NotBeNull();
		company.DomainName.Should().Be("panoramicdata.com");
		company.DomainStatus.Should().Be("clientTransferProhibited");
		company.Registrar.Should().NotBeNullOrWhiteSpace();
	}
}