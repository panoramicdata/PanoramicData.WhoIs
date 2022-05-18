using FluentAssertions;
using System.Net.Mail;

namespace EmailLookup.Test
{
   public class UnitTest1 : TestBase
   {
	  [Fact]
	  public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
	  {
		 var response = await GoogleSearcher
			.SearchLinkedInAsync(new Person(new MailAddress("david.bond@panoramicdata.com")))
			.ConfigureAwait(false);

		 response.Should().NotBeNull();
		 response!.Title.Should().NotBeNullOrEmpty();
		 response.Url.Should().NotBeNullOrEmpty();
		 response.Description.Should().NotBeNullOrEmpty();
	  }
   }
}