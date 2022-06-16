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
		 response.FullName.Should().Be("David Bond");
		}

        [Fact]
        public async void ValidEmailSearch_ShouldReturnGoogleResults()
        {
            var response = await GoogleSearcher
                .SearchGoogleAsync(new Person(new MailAddress("david.bond@panoramicdata.com")), default)
                .ConfigureAwait(false);

            response.Should().NotBeNull();
        }
   }
}