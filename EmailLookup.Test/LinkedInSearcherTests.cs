using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
		{
			var testPerson = new Person("david.bond@panoramicdata.com");

			var response = await LinkedInSearcher
			.SearchAsync(testPerson)
			.ConfigureAwait(false);

			response.Should().NotBeNull();
			response.FirstName.Should().Be("David");
		}

		[Fact]
		public async void ValidEmailSearch_ShouldReturnGoogleResults()
		{
			var response = await LinkedInSearcher
				.SearchGoogleAsync("david.bond@panoramicdata.com", default)
				.ConfigureAwait(false);

			response.Should().NotBeNull();
		}
	}
}