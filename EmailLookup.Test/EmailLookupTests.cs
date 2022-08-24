using FluentAssertions;
using System.Net.Mail;

namespace EmailLookup.Test
{
   public class EmailLookupTests : TestBase
   {
	  [Fact]
	  public async void ValidSearch_ShouldReturnResponse()
	  {
			//var response = await EmailLookup
			//	.LookupAsync("david.bond@panoramicdata.com", default)
			//	.ConfigureAwait(false);
			
			//response.Should().NotBeNull();
			//response.WhoIs.Should().NotBeNull();
			//response.LinkedIn.Should().NotBeNull();
	  }
   }
}