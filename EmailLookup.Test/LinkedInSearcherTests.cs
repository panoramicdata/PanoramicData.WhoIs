using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void LinkedInSearcher_ShouldReturnResults()
		{
			var response = await ProxyCurlSearcher
				.SearchAsync(new Person("david.bond@panoramicdata.com"))
				.ConfigureAwait(false);

			response.FirstName.Should().Be("David");
		}
	}
}