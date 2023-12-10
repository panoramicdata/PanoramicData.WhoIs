using PanoramicData.WhoIs.Extensions;
using PanoramicData.WhoIs.Interfaces;

namespace PanoramicData.WhoIs.Enhancers;

public abstract class BasicPersonEnhancer : IPersonEnhancer
{
	public abstract Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken);

	protected internal static Person BasicEnhance(Person person)
	{
		Person workingPerson = person;
		var mailAddress = person.MailAddress;
		if (mailAddress is not null)
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

		}

		return workingPerson;
	}

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

	public virtual IReadOnlyCollection<ICompanyEnhancer> CompanyEnhancers => [];
}