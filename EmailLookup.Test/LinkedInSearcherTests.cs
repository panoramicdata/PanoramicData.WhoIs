using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void ValidEmailSearch_ShouldReturnLinkedInPageDetails()
		{

			if (TestEmail is not null)
			{
				var testPerson = new Person(TestEmail);

				var response = await ProxyCurlSearcher
					.SearchAsync(testPerson)
					.ConfigureAwait(false);
				Console.WriteLine(response.FirstName);

				response.FirstName.Should().Be("David");
			}
		}
	}
}