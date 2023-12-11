using PanoramicData.WhoIs.Enhancers;
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Test.Fakes;

public class FakePersonEnhancer : BasicPersonEnhancer
{
	private readonly Person _fakePerson = new();

	public FakePersonEnhancer()
	{
	}

	public FakePersonEnhancer(Person fakePerson)
	{
		_fakePerson = fakePerson;
	}

	public override IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers => [];

	public override Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken)
		=> Task.FromResult(Merge(person, _fakePerson));
}
