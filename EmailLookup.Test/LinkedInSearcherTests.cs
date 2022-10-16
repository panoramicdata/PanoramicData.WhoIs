using EmailLookup.Core;
using EmailLookup.CustomExceptions;
using FluentAssertions;
using System.Runtime.CompilerServices;

namespace EmailLookup.Test
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void LinkedInSearcher_ShouldReturnResults()
		{
			var response = await ProxyCurlSearcher
				.SearchAsync(new Person(TEmail))
				.ConfigureAwait(false);

			response.FirstName.Should().Be("David");
		}

		[Fact]
		public async void LinkedInSearcher_WithInvalidEmail_ShouldThrowException()
		{
			Func<Task> getResponse = async () => {
				await ProxyCurlSearcher
				.PersonProfileLookupAsync("https://www.linkedin.com/in/thisprofiledoesnotexist", default)
				.ConfigureAwait(false);
			};

			await getResponse.Should().ThrowAsync<ProxyCurlNotFoundException>();
		}
	}
}