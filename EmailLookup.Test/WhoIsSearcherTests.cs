using FluentAssertions;

namespace EmailLookup.Test
{
   public class WhoIsSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidDomainSearch_ShouldReturnWhoIsResponse()
	  {
		 var response = await new WhoIsSearcher()
			.GetResponseAsync("panoramicdata.com", default)
			.ConfigureAwait(false);

		response.Should().NotBeNull();
	  }
   }
}