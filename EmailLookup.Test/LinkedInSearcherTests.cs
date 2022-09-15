using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
		{

			if (TestEmailTwo is not null)
			{
				var testPerson = new Person(TestEmailTwo);

				var response = await ProxyCurlSearcher
					.SearchAsync(testPerson)
					.ConfigureAwait(false);

				response.Should().NotBeNull();
			}
		}
	}
}