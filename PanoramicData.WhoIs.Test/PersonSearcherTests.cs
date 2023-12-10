using FluentAssertions;
using PanoramicData.WhoIs.Enhancers;
using System.Net.Mail;

namespace PanoramicData.WhoIs.Test;

public class PersonSearcherTests : TestBase
{
	[Fact]
	public async Task PersonSearcher_WithInvalidEmailAddress_ShouldThrowException()
	{
		var searcher = new PersonEnhancer([FakeSearcher]);

		var getResponse = async () =>
		{
			await searcher
			.EnhanceAsync(new Person { MailAddress = new MailAddress("asdfghjkl") }, default)
			.ConfigureAwait(false);
		};
		await getResponse.Should().ThrowAsync<FormatException>();
	}

	[Fact]
	public async Task PersonSearcher_WithTwoSearchers_ShouldPrioritizeFirstSearcherResults()
	{
		var searcher = new PersonEnhancer([FakeSearcher, AnotherFakeSearcher]);

		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.FirstName.Should().Be("first");
	}

	[Fact]
	public async Task ProfileMerger_MergingTwoProfilesWithPopulatedLanguageLists_CanMergeSuccessfully()
	{
		var searcher = new PersonEnhancer([FakeSearcher, AnotherFakeSearcher]);
		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.Languages.Should().Contain("english");
		response.Languages.Should().Contain("french");
	}

	[Fact]
	public async Task ProfileMerger_IfFirstProfileNotFound_SecondShouldOverride()
	{
		var searcher = new PersonEnhancer([NoEnhancementSearcher, FakeSearcher]);
		var response = await searcher
			.EnhanceAsync(new Person { MailAddress = exampleEmail }, default)
			.ConfigureAwait(false);
		response.FirstName.Should().Be("first");
	}
}