using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
	{
		[Fact]
		public async void PersonSearcher_ShouldReturnResults()
		{
			var response = await PersonSearcher
				.LookupProfileAsync(TEmail)
				.ConfigureAwait(false);

			response.Profile.FirstName.Should().Be("David");
		}
	}
}