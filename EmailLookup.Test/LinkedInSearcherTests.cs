using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
		{
			var testPerson = new Person("satish.margabandhu@genpact.com");

			var response = await ProxyCurlSearcher
				.SearchAsync(testPerson)
				.ConfigureAwait(false);

			response.Should().NotBeNull();
		}

		[Fact]
		public async void ValidEmailSearch_ShouldReturnGoogleResults()
		{
			var response = await ProxyCurlSearcher
				.SearchGoogleAsync("david.bond@panoramicdata.com", default)
				.ConfigureAwait(false);

			response.Should().NotBeNull();
		}
	}
}