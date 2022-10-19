using EmailLookup.Core;
using EmailLookup.CustomExceptions;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class InvalidEmailTests : TestBase
	{
		[Fact]
		public async void LookupProfileAsync_NotAnEmailAddress_ShouldThrowInvalidEmailException()
		{
			Func<Task> getResponse = async () =>
			{
				await PersonSearcher
				.LookupProfileAsync("asdfghjkl")
				.ConfigureAwait(false);
			};

			await getResponse.Should().ThrowAsync<InvalidEmailException>();
		}

		[Fact]
		public async void LookupProfileAsync_FakeEmailAddress_ShouldThrowNoProfileException()
		{
			Func<Task> getResponse = async () =>
			{
				await PersonSearcher
				.LookupProfileAsync("fakename.daniels@hotmail.com")
				.ConfigureAwait(false);
			};

			await getResponse.Should().ThrowAsync<NoProfileException>();
		}
	}
}