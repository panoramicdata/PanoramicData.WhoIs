using EmailLookup.Core;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
   {
	  [Fact]
	  public async void ValidSearch_ShouldReturnResponse()
		{
			if (TestEmail is not null)
			{
				var response = await PersonSearcher
					.LookupProfileAsync(TestEmail)
					.ConfigureAwait(false);

				response.Should().NotBeNull();
			}
		}
   }
}