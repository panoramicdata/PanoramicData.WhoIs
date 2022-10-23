using EmailLookup.Core;
using EmailLookup.Core.ProxyCurl;
using EmailLookup.CustomExceptions;
using FluentAssertions;

namespace EmailLookup.IntegrationTest
{
	public class LinkedInSearcherTests : TestBase
	{
		[Fact]
		public async void LinkedInSearcher_WithValidEmailAddress_ShouldReturnResults()
		{
			var response = await ProxyCurlSearcher
				.SearchAsync(new Person(ValidEmailAddress))
				.ConfigureAwait(false);

			response.FirstName.Should().Be(ValidFirstname);
		}

		[Fact]
		public async void LinkedInSearcher_WithInvalidEmail_ShouldThrowException()
		{
			Func<Task> getResponse = async () =>
			{
				await ProxyCurlSearcher
				.PersonProfileLookupAsync("https://www.linkedin.com/in/thisprofiledoesnotexist", default)
				.ConfigureAwait(false);
			};

			await getResponse.Should().ThrowAsync<ProxyCurlException>();
		}

		[Fact]
		public async void LinkedInSearcher_WithInvalidKey_ShouldThrowException()
		{
			var badKeySearcher = new ProxyCurlSearcher(new ProxyCurlConfig
			{
				GoogleCx = "invalid",
				GoogleKey = "invalid",
				ProxyCurlKey = "invalid"
			});

			Func<Task> getResponse = async () =>
			{
				await badKeySearcher
				.PersonProfileLookupAsync(ValidProfileUrl, default)
				.ConfigureAwait(false);
			};

			await getResponse.Should().ThrowAsync<ProxyCurlException>();
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

			await getResponse.Should().ThrowAsync<ProxyCurlException>();
		}
	}
}