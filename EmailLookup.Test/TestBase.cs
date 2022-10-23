using EmailLookup.Core.ProxyCurl;
using EmailLookup.ProfileResult;
using EmailLookup.Test.Fakes;
using Microsoft.Extensions.Configuration;

namespace EmailLookup.Test;

public abstract class TestBase
{
	protected FakePersonSearcher fakeSearcher { get; }
	protected FakePersonSearcher anotherFakeSearcher { get; }

	public TestBase()
	{
		fakeSearcher = new FakePersonSearcher(new Profile
		{
			FirstName = "first",
			Languages = new List<string>() { "english" },
			Outcome = Core.LookupOutcomes.Found
		});
		anotherFakeSearcher = new FakePersonSearcher(new Profile
		{
			FirstName = "second",
			Languages = new List<string>() { "french" },
			Outcome = Core.LookupOutcomes.Found
		});
	}

}