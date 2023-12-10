using FluentAssertions;
using PanoramicData.WhoIs;
using PanoramicData.WhoIs.Enhancers;

namespace PanoramicData.HumanWhoIs.IntegrationTest;

public class WhoIsSearcherTests : TestBase
{
	[Fact]
	public async Task ValidDomainSearch_ShouldReturnWhoIsResponse()
	{
		var response = await new WhoIsPersonEnhancer()
			.EnhanceAsync(new Person
			{
				MailAddress = ValidMailAddress
			}, default)
			.ConfigureAwait(false);

		response.Should().NotBeNull();
	}
}