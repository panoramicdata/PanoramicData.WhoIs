using EmailLookup.Core;
using EmailLookup.Core.WhoIs;
using FluentAssertions;

namespace EmailLookup.IntegrationTest;

public class WhoIsSearcherTests : TestBase
{
	[Fact]
	public async Task ValidDomainSearch_ShouldReturnWhoIsResponse()
	{
		var response = await new WhoIsSearcher()
		   .SearchAsync(new Person(ValidEmailAddress))
		   .ConfigureAwait(false);

		response.Should().NotBeNull();
	}
}