using FluentAssertions;
using PanoramicData.WhoIs;
using PanoramicData.WhoIs.CustomExceptions;
using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.ProxyCurl;

namespace PanoramicData.HumanWhoIs.IntegrationTest;

public class LinkedInSearcherTests : TestBase
{
	[Fact]
	public async Task LinkedInSearcher_WithValidEmailAddress_ShouldReturnResults()
	{
		var response = await ProxyCurlPersonEnhancer
			.EnhanceAsync(new Person { MailAddress = ValidMailAddress }, default)
			.ConfigureAwait(false);

		response.FirstName.Should().Be(ValidFirstName);
	}

	[Fact]
	public async Task LinkedInSearcher_WithInvalidEmail_ShouldThrowException()
	{
		Func<Task> getResponse = async () =>
		{
			await ProxyCurlPersonEnhancer
			.EnhanceAsync(new Person
			{
				LinkedInUrl = "https://www.linkedin.com/in/thisprofiledoesnotexist"
			}, default)
			.ConfigureAwait(false);
		};

		await getResponse.Should().ThrowAsync<ProxyCurlException>();
	}

	[Fact]
	public async Task LinkedInSearcher_WithInvalidKey_ShouldThrowException()
	{
		var badKeySearcher = new ProxyCurlPersonEnhancer(new ProxyCurlConfig
		{
			GoogleCx = "invalid",
			GoogleKey = "invalid",
			ProxyCurlKey = "invalid"
		});

		Func<Task> getResponse = async () =>
		{
			await badKeySearcher
			.EnhanceAsync(
				new Person
				{
					LinkedInUrl = ValidProfileUrl
				}, default)
			.ConfigureAwait(false);
		};

		await getResponse.Should().ThrowAsync<ProxyCurlException>();
	}
	[Fact]
	public async Task LookupProfileAsync_FakeEmailAddress_ShouldThrowNoProfileException()
	{
		Func<Task> getResponse = async () =>
		{
			await PersonEnhancer
			.EnhanceAsync
			(new Person
			{
				MailAddress = new("fakename.daniels@hotmail.com")
			}, default)
			.ConfigureAwait(false);
		};

		await getResponse.Should().ThrowAsync<ProxyCurlException>();
	}
}