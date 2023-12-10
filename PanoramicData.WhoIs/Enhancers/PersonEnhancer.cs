using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// Performs lookups on real people and companies.
/// </summary>
/// <remarks>
/// <para>This class is responsible for creating every other object used in the
/// process, and determines the order in which the program executes.</para>
/// </remarks>
/// <param name="searchers">A list of objects that use the IPersonSearcher interface.</param>
/// <remarks>
/// <para> Users can choose which searchers to include in their lookup by passing them into
/// the searchers parameter.</para>
/// </remarks>
public class PersonEnhancer(IEnumerable<IPersonEnhancer> personEnhancers) : IDisposable
{
	private bool _disposedValue;
	private readonly IEnumerable<IPersonEnhancer> _personEnhancers = personEnhancers;

	/// <summary>
	/// Takes as much as you know about a person, passes it to the list of searchers and returns a more complete version.
	/// </summary>
	/// <param name="mailAddress">The email address the user wants to lookup.</param>
	/// <returns>A SearchResult object containing information on the inputted email
	/// address.</returns>
	public async Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken)
	{
		Person workingPerson = person;
		foreach (var personEnhancer in _personEnhancers)
		{
			workingPerson = await personEnhancer
				.EnhanceAsync(workingPerson, cancellationToken);

			if (workingPerson.Company is null)
			{
				continue;
			}

			foreach (var companyEnhancer in personEnhancer.CompanyEnhancers)
			{
				workingPerson.Company = await companyEnhancer.EnhanceAsync(workingPerson.Company, cancellationToken);
			}
		}

		return workingPerson;
	}

	protected virtual void Dispose(bool disposing)
	{
		if (!_disposedValue)
		{
			if (disposing)
			{
				// TODO: dispose managed state (managed objects)
			}

			// TODO: free unmanaged resources (unmanaged objects) and override finalizer
			// TODO: set large fields to null
			_disposedValue = true;
		}
	}

	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
