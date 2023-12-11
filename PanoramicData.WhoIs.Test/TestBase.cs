using PanoramicData.WhoIs.Test.Fakes;
using System.Net.Mail;

namespace PanoramicData.WhoIs.Test;

public abstract class TestBase
{
	protected FakePersonEnhancer FakeSearcher { get; }

	protected FakePersonEnhancer AnotherFakeSearcher { get; }

	protected FakePersonEnhancer NoEnhancementSearcher { get; }

	protected MailAddress exampleEmail { get; } = new("example@hotmail.com");

	protected TestBase()
	{
		FakeSearcher = new FakePersonEnhancer(new Person
		{
			FirstName = "first",
			Languages = ["english"],
		});

		AnotherFakeSearcher = new FakePersonEnhancer(new Person
		{
			FirstName = "second",
			Languages = ["french"],
		});

		NoEnhancementSearcher = new FakePersonEnhancer(new Person());
	}
}