using PanoramicData.WhoIs.Extensions;
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// Abstract base class for person enhancers, providing shared enrichment logic
/// that populates basic person and company fields from the email address.
/// </summary>
public abstract class BasicPersonEnhancer : IPersonEnhancer
{
	/// <summary>
	/// Enriches the specified <see cref="Person"/> with additional information
	/// from the data source implemented by the derived class.
	/// </summary>
	/// <param name="person">The person to enrich.</param>
	/// <param name="cancellationToken">A token to cancel the asynchronous operation.</param>
	/// <returns>A <see cref="Person"/> populated with as much information as the source provides.</returns>
	public abstract Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken);

	/// <summary>
	/// Performs basic enrichment of a <see cref="Person"/> using only the email address,
	/// inferring first name, last name, and company domain without calling any external service.
	/// </summary>
	/// <param name="person">The person to enrich.</param>
	/// <returns>A <see cref="Person"/> with name and company fields populated where possible.</returns>
	protected internal static Person BasicEnhance(Person person)
	{
		Person workingPerson = person;
		var mailAddress = person.MailAddress;
		if (mailAddress is null)
		{
			return workingPerson;
		}

		workingPerson = EnrichFromMailAddress(workingPerson, mailAddress);
		workingPerson = EnrichCompanyDomain(workingPerson, mailAddress);

		return workingPerson;
	}

	private static Person EnrichFromMailAddress(Person workingPerson, System.Net.Mail.MailAddress mailAddress)
	{
		if (workingPerson.FirstName is null)
		{
			workingPerson = Merge(workingPerson, new Person
			{
				FirstName = mailAddress.User[..mailAddress.User.IndexOf('.')].ToPascalCase(),
			});
		}

		if (workingPerson.LastName is null)
		{
			workingPerson = Merge(workingPerson, new Person
			{
				LastName = mailAddress.User[(mailAddress.User.IndexOf('.') + 1)..].ToPascalCase(),
			});
		}

		return workingPerson;
	}

	private static Person EnrichCompanyDomain(Person workingPerson, System.Net.Mail.MailAddress mailAddress)
	{
		workingPerson.Company ??= new Company
		{
			DomainName = mailAddress.Host,
		};

		workingPerson.Company = new NameFinderCompanyEnhancer()
			.EnhanceAsync(workingPerson.Company, default).GetAwaiter().GetResult();

		if (workingPerson.Company.DomainName is null)
		{
			workingPerson.Company = BasicCompanyEnhancer.Merge(workingPerson.Company, new Company
			{
				DomainName = mailAddress.Host,
			});
		}

		return workingPerson;
	}

	/// <summary>
	/// Merges two <see cref="Person"/> instances, preferring non-null values from <paramref name="sourcePerson"/>
	/// and falling back to values from <paramref name="newInformationPerson"/> for any fields that are missing.
	/// Collection properties are merged as the distinct union of both sources.
	/// </summary>
	/// <param name="sourcePerson">The existing person record, whose values take precedence.</param>
	/// <param name="newInformationPerson">The newly enriched person record providing additional data.</param>
	/// <returns>A new <see cref="Person"/> with fields combined from both sources.</returns>
	protected static Person Merge(Person sourcePerson, Person newInformationPerson)
	{
		var person = new Person();

		MergeIdentity(person, sourcePerson, newInformationPerson);
		MergeLocation(person, sourcePerson, newInformationPerson);
		MergeEmployment(person, sourcePerson, newInformationPerson);
		MergeCollections(person, sourcePerson, newInformationPerson);

		return person;
	}

	private static void MergeIdentity(Person target, Person source, Person newInformation)
	{
		target.FirstName = source.FirstName ?? newInformation.FirstName;
		target.LastName = source.LastName ?? newInformation.LastName;
		target.Gender = source.Gender ?? newInformation.Gender;
		target.Age = source.Age ?? newInformation.Age;
		target.BirthYear = source.BirthYear ?? newInformation.BirthYear;
		target.MailAddress = source.MailAddress ?? newInformation.MailAddress;
	}

	private static void MergeLocation(Person target, Person source, Person newInformation)
	{
		target.Country = source.Country ?? newInformation.Country;
		target.City = source.City ?? newInformation.City;
		target.State = source.State ?? newInformation.State;
	}

	private static void MergeEmployment(Person target, Person source, Person newInformation)
	{
		target.Occupation = source.Occupation ?? newInformation.Occupation;
		target.InferredSalaryMin = source.InferredSalaryMin ?? newInformation.InferredSalaryMin;
		target.InferredSalaryMax = source.InferredSalaryMax ?? newInformation.InferredSalaryMax;
		target.Company = MergeCompanies(source.Company, newInformation.Company);
	}

	private static Company? MergeCompanies(Company? source, Company? newInformation)
		=> (source, newInformation) switch
		{
			(null, _) => newInformation,
			(_, null) => source,
			_ => BasicCompanyEnhancer.Merge(source, newInformation),
		};

	private static void MergeCollections(Person target, Person source, Person newInformation)
	{
		target.Awards = Combine(source.Awards, newInformation.Awards);
		target.Courses = Combine(source.Courses, newInformation.Courses);
		target.Education = Combine(source.Education, newInformation.Education);
		target.Experiences = Combine(source.Experiences, newInformation.Experiences);
		target.Languages = Combine(source.Languages, newInformation.Languages);
		target.PersonalEmails = Combine(source.PersonalEmails, newInformation.PersonalEmails);
		target.PersonalNumbers = Combine(source.PersonalNumbers, newInformation.PersonalNumbers);
		target.Projects = Combine(source.Projects, newInformation.Projects);
	}

	private static List<T> Combine<T>(IEnumerable<T> source, IEnumerable<T> newInformation)
		=> source.Union(newInformation).Distinct().ToList();

	/// <summary>
	/// The collection of company enhancers used alongside this person enhancer.
	/// Returns an empty collection by default; derived classes may override this.
	/// </summary>
	public virtual IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers => [];
}