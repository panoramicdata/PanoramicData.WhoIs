using FluentAssertions;

namespace EmailLookup.Test
{
   public class WhoIsSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidDomainSearch_ShouldReturnWhoIsResponse()
	  {
		 var response = await new WhoIsSearcher()
			.SearchAsync(new Person("david.bond@panoramicdata.com"))
			.ConfigureAwait(false);

		response.Should().NotBeNull();
	  }
   }
}