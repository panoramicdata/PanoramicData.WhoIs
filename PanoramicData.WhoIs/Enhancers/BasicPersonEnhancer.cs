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
	protected static Person Merge(Person sourcePerson, Person newInformationPerson) => new()
	{
		Age = sourcePerson.Age ?? newInformationPerson.Age,
		Awards = sourcePerson.Awards.Union(newInformationPerson.Awards).Distinct().ToList(),
		Courses = sourcePerson.Courses.Union(newInformationPerson.Courses).Distinct().ToList(),
		Country = sourcePerson.Country ?? newInformationPerson.Country,
		Education = sourcePerson.Education.Union(newInformationPerson.Education).Distinct().ToList(),
		FirstName = sourcePerson.FirstName ?? newInformationPerson.FirstName,
		LastName = sourcePerson.LastName ?? newInformationPerson.LastName,
		Gender = sourcePerson.Gender ?? newInformationPerson.Gender,
		Occupation = sourcePerson.Occupation ?? newInformationPerson.Occupation,
		Experiences = sourcePerson.Experiences.Union(newInformationPerson.Experiences).Distinct().ToList(),
		InferredSalaryMax = sourcePerson.InferredSalaryMax ?? newInformationPerson.InferredSalaryMax,
		InferredSalaryMin = sourcePerson.InferredSalaryMin ?? newInformationPerson.InferredSalaryMin,
		Languages = sourcePerson.Languages.Union(newInformationPerson.Languages).Distinct().ToList(),
		PersonalEmails = sourcePerson.PersonalEmails.Union(newInformationPerson.PersonalEmails).Distinct().ToList(),
		PersonalNumbers = sourcePerson.PersonalNumbers.Union(newInformationPerson.PersonalNumbers).Distinct().ToList(),
		Company = sourcePerson.Company is null
			? newInformationPerson.Company
			: newInformationPerson.Company is null ? null
			: BasicCompanyEnhancer.Merge(sourcePerson.Company, newInformationPerson.Company),
		MailAddress = sourcePerson.MailAddress ?? newInformationPerson.MailAddress,
		BirthYear = sourcePerson.BirthYear ?? newInformationPerson.BirthYear,
		City = sourcePerson.City ?? newInformationPerson.City,
		State = sourcePerson.State ?? newInformationPerson.State,
		Projects = sourcePerson.Projects.Union(newInformationPerson.Projects).Distinct().ToList(),
	};

	/// <summary>
	/// The collection of company enhancers used alongside this person enhancer.
	/// Returns an empty collection by default; derived classes may override this.
	/// </summary>
	public virtual IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers => [];
}