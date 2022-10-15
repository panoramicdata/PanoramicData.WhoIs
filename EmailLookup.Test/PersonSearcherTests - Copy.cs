using EmailLookup.Core;
using EmailLookup.CustomExceptions;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class InvalidEmailTests : TestBase
	{
		[Fact]
		public async void NotAnEmail_ShouldThrowException()
		{
			Func<Task> getResponse = async () => { await PersonSearcher.LookupProfileAsync("asdfghjkl").ConfigureAwait(false); };

			await getResponse.Should().ThrowAsync<InvalidEmailException>();
		}
	}
}