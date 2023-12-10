using PanoramicData.WhoIs.Test.Fakes;
using System.Net.Mail;

namespace PanoramicData.WhoIs.Test;

public abstract class TestBase
{
	protected FakePersonSearcher FakeSearcher { get; }

	protected FakePersonSearcher AnotherFakeSearcher { get; }

	protected FakePersonSearcher NoEnhancementSearcher { get; }

	protected MailAddress exampleEmail { get; } = new("example@hotmail.com");

	protected TestBase()
	{
		FakeSearcher = new FakePersonSearcher(new Person
		{
			FirstName = "first",
			Languages = ["english"],
		});

		AnotherFakeSearcher = new FakePersonSearcher(new Person
		{
			FirstName = "second",
			Languages = ["french"],
		});

		NoEnhancementSearcher = new FakePersonSearcher(new Person());
	}
}