namespace PanoramicData.WhoIs.Interfaces;

public interface IPersonEnhancer
{
	/// <summary>
	/// Enhances the person.
	/// <summary>
	Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken);

	IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers { get; }
}