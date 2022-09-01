using EmailLookup.Core;
using FluentAssertions;
using System.Net.Mail;

namespace EmailLookup.Test
{
   public class PersonSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidSearch_ShouldReturnResponse()
		{
			var response = await PersonSearcher
				.LookupProfileAsync("david.bond@panoramicdata.com")
				.ConfigureAwait(false);

			response.Should().NotBeNull();
		}
   }
}