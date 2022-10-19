using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
	{
		[Fact]
		public async void PersonSearcher_WithValidEmailAddress_ShouldReturnResults()
		{
			var response = await PersonSearcher
				.LookupProfileAsync(ValidEmailAddress)
				.ConfigureAwait(false);

			response.Profile.FirstName.Should().Be(ValidFirstname);
		}
	}
}