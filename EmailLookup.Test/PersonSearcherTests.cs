using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidSearch_ShouldReturnResponse()
		{
			if (TestEmailOne is not null)
			{
				var response = await PersonSearcher
					.LookupProfileAsync(TestEmailOne)
					.ConfigureAwait(false);

				response.Should().NotBeNull();
			}
		}
   }
}