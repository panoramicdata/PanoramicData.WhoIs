using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Enhancers.ProxyCurl;
using PanoramicData.WhoIs.Exceptions;

namespace PanoramicData.WhoIs.IntegrationTest;

public class LinkedInSearcherTests : TestBase
{
	[Fact]
	public async Task LinkedInSearcher_WithValidEmailAddress_ShouldReturnResults()
	{
		var response = await ProxyCurlPersonEnhancer
			.EnhanceAsync(new Person { MailAddress = ValidMailAddress }, CancellationToken);

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
			}, CancellationToken);
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
				}, CancellationToken);
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
			}, CancellationToken);
		};

		await getResponse.Should().ThrowAsync<ProxyCurlException>();
	}
}