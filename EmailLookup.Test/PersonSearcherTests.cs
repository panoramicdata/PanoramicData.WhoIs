using EmailLookup.Core;
using EmailLookup.ProfileResult;
using EmailLookup.Test.Fakes;
using FluentAssertions;

namespace EmailLookup.IntegrationTest
{
	public class PersonSearcherTests : TestBase
	{
		[Fact]
		public async void PersonSearcher_WithValidEmailAddress_ShouldReturnResults()
		{
			FakePersonSearcher fakeSearcher = new FakePersonSearcher(new Profile
			{
				FirstName = ValidFirstname,
				Outcome = LookupOutcomes.Found
			});

			IEnumerable<IPersonSearcher> _searchers = new List<IPersonSearcher>() { fakeSearcher };

			PersonSearcher searcher = new PersonSearcher(_searchers);

			var response = await searcher
				.LookupProfileAsync(ValidEmailAddress)
				.ConfigureAwait(false);

			response.Profile.FirstName.Should().Be(ValidFirstname);
		}

		[Fact]
		public async void PersonSearcher_WithTwoSearchers_ShouldPrioritiseFirstSearcherResults()
		{
			FakePersonSearcher fakeSearcher = new FakePersonSearcher(new Profile
			{
				FirstName = "first",
				Outcome = LookupOutcomes.Found
			});
			FakePersonSearcher anotherFakeSearcher = new FakePersonSearcher(new Profile
			{
				FirstName = "second",
				Outcome = LookupOutcomes.Found
			});

			IEnumerable<IPersonSearcher> _searchers = new List<IPersonSearcher>() { fakeSearcher, anotherFakeSearcher };

			PersonSearcher searcher = new PersonSearcher(_searchers);

			var response = await searcher
				.LookupProfileAsync(ValidEmailAddress)
				.ConfigureAwait(false);
			response.Profile.FirstName.Should().Be("first");
		}
	}
}