﻿using EmailLookup.Core;
using EmailLookup.CustomExceptions;
using EmailLookup.ProfileResult;
using EmailLookup.Test.Fakes;
using FluentAssertions;

namespace EmailLookup.Test
{
	public class PersonSearcherTests : TestBase
	{
		[Fact]
		public async void PersonSearcher_WithValidEmailAddress_ShouldReturnResults()
		{
			PersonSearcher searcher = new PersonSearcher(new List<IPersonSearcher>() { fakeSearcher });

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
			PersonSearcher searcher = new PersonSearcher(new List<IPersonSearcher>() { fakeSearcher, anotherFakeSearcher});

			var response = await searcher
				.LookupProfileAsync("example@hotmail.com")
				.ConfigureAwait(false);
			response.Profile.FirstName.Should().Be("first");
		}

		[Fact]
		public async void ProfileMerger_MergingTwoProfilesWithPopulatedLanguageLists_CanMergeSuccessfully()
		{
			PersonSearcher searcher = new PersonSearcher(new List<IPersonSearcher>() { fakeSearcher, anotherFakeSearcher });
			var response = await searcher
				.LookupProfileAsync("example@hotmail.com")
				.ConfigureAwait(false);
			response.Profile.Languages.Should().Contain("english");
			response.Profile.Languages.Should().Contain("french");
		}
	}
}