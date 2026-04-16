using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// Performs lookups on real people and companies.
/// </summary>
/// <remarks>
/// <para>This class is responsible for orchestrating every enhancer used in the
/// process, and determines the order in which they execute.</para>
/// <para>Users can choose which enhancers to include in their lookup by passing them into
/// the <paramref name="personEnhancers"/> parameter.</para>
/// </remarks>
/// <param name="personEnhancers">A list of <see cref="IPersonEnhancer"/> implementations to apply in sequence.</param>
public class PersonEnhancer(IEnumerable<IPersonEnhancer> personEnhancers) : IDisposable
{
	private bool _disposedValue;
	private readonly IEnumerable<IPersonEnhancer> _personEnhancers = personEnhancers;

	/// <summary>
	/// Applies each registered person enhancer in sequence to produce an enriched <see cref="Person"/>,
	/// then applies the company enhancers associated with each person enhancer.
	/// </summary>
	/// <param name="person">The person to enrich.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Person"/> with all available enrichment applied.</returns>
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

	/// <summary>
	/// Releases managed and unmanaged resources held by this instance.
	/// </summary>
	/// <param name="disposing">
	/// <see langword="true"/> to release both managed and unmanaged resources;
	/// <see langword="false"/> to release only unmanaged resources.
	/// </param>
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

	/// <summary>
	/// Releases all resources used by this <see cref="PersonEnhancer"/>.
	/// </summary>
	public void Dispose()
	{
		// Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
