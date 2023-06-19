using EmailLookup.ProfileResult;
using EmailLookup.Test.Fakes;

namespace EmailLookup.Test;

public abstract class TestBase
{
	protected FakePersonSearcher FakeSearcher { get; }
	protected FakePersonSearcher AnotherFakeSearcher { get; }
	protected FakePersonSearcher NotFoundSearcher { get; }
	protected string exampleEmail { get; } = string.Empty;

	protected TestBase()
	{
		FakeSearcher = new FakePersonSearcher(new Profile
		{
			FirstName = "first",
			Languages = new List<string>() { "english" },
			Outcome = Core.LookupOutcomes.Found
		});
		AnotherFakeSearcher = new FakePersonSearcher(new Profile
		{
			FirstName = "second",
			Languages = new List<string>() { "french" },
			Outcome = Core.LookupOutcomes.Found
		});
		NotFoundSearcher = new FakePersonSearcher(new Profile
		{
			FirstName = "",
			Outcome = Core.LookupOutcomes.NotFound
		});
		exampleEmail = "example@hotmail.com";
	}

}