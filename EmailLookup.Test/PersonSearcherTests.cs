using EmailLookup.Core;
using EmailLookup.CustomExceptions;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
	{
		[Fact]
		public async void PersonSearcher_WithInvalidEmailAddress_ShouldThrowException()
		{
			var searcher = new PersonSearcher(new List<IPersonSearcher>() { FakeSearcher });

			Func<Task> getResponse = async () =>
			{
				await searcher
				.LookupProfileAsync("asdfghjkl")
				.ConfigureAwait(false);
			};
			await getResponse.Should().ThrowAsync<InvalidEmailException>();
		}

		[Fact]
		public async void PersonSearcher_WithTwoSearchers_ShouldPrioritiseFirstSearcherResults()
		{
			var searcher = new PersonSearcher(new List<IPersonSearcher>() { FakeSearcher, AnotherFakeSearcher });

			var response = await searcher
				.LookupProfileAsync(exampleEmail)
				.ConfigureAwait(false);
			response.Profile.FirstName.Should().Be("first");
		}

		[Fact]
		public async void ProfileMerger_MergingTwoProfilesWithPopulatedLanguageLists_CanMergeSuccessfully()
		{
			var searcher = new PersonSearcher(new List<IPersonSearcher>() { FakeSearcher, AnotherFakeSearcher });
			var response = await searcher
				.LookupProfileAsync(exampleEmail)
				.ConfigureAwait(false);
			response.Profile.Languages.Should().Contain("english");
			response.Profile.Languages.Should().Contain("french");
		}

		[Fact]
		public async void ProfileMerger_IfFirstProfileNotFound_SecondShouldOverride()
		{
			var searcher = new PersonSearcher(new List<IPersonSearcher>() { NotFoundSearcher, FakeSearcher });
			var response = await searcher
				.LookupProfileAsync(exampleEmail)
				.ConfigureAwait(false);
			response.Profile.FirstName.Should().Be("first");
		}
	}
}