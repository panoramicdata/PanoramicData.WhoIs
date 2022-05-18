using FluentAssertions;
using System.Net.Mail;

namespace EmailLookup.Test
{
   public class GoogleSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
	  {
		 var response = await GoogleSearcher
			.SearchLinkedInAsync(new Person(new MailAddress("david.bond@panoramicdata.com")), default)
			.ConfigureAwait(false);

		 response.Should().NotBeNull();
		 response!.Jobs.Should().NotBeNull();
		 response.Description.Should().NotBeNull();
	  }
   }
}