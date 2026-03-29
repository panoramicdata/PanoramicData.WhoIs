using PanoramicData.WhoIs.ProfileResult;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

internal static class DetailedPersonInformationExtensions
{

	internal static Person ToProfile(this DetailedPersonInformation personInformation)
	{
		var profile = new Person
		{
			FirstName = personInformation.FirstName,
			LastName = personInformation.LastName,
			Gender = personInformation.Gender,
			Occupation = personInformation.Occupation,
			Country = personInformation.CountryFullName,
			City = personInformation.City,
			State = personInformation.State,
			Languages = personInformation.Languages,
			PersonalEmails = personInformation.PersonalEmails,
			PersonalNumbers = personInformation.PersonalNumbers,
		};

		ApplyBirthDate(profile, personInformation);
		MapExperiences(profile, personInformation);
		MapEducation(profile, personInformation);
		MapAwards(profile, personInformation);
		MapCourses(profile, personInformation);
		MapProjects(profile, personInformation);

		return profile;
	}

	private static void ApplyBirthDate(Person profile, DetailedPersonInformation info)
	{
		if (info.BirthDate is null)
		{
			return;
		}

		profile.BirthYear = info.BirthDate.Year;
		if (info.BirthDate.Year != 0)
		{
			profile.Age = DateTime.Now.Year - info.BirthDate.Year;
		}
	}

	private static void MapExperiences(Person profile, DetailedPersonInformation info)
	{
		foreach (var experience in info.Experiences)
		{
			profile.Experiences.Add(new ProfileExperiences
			{
				Company = experience.Company,
				Title = experience.Title,
				Description = experience.Description,
				Location = experience.Location
			});
		}
	}

	private static void MapEducation(Person profile, DetailedPersonInformation info)
	{
		foreach (var education in info.Education)
		{
			profile.Education.Add(new ProfileEducation
			{
				FieldOfStudy = education.FieldOfStudy,
				DegreeName = education.DegreeName,
				School = education.School,
				Description = education.Description
			});
		}
	}

	private static void MapAwards(Person profile, DetailedPersonInformation info)
	{
		foreach (var award in info.AccomplishmentHonorsAwards)
		{
			profile.Awards.Add(new ProfileAwards
			{
				Title = award.Title,
				Issuer = award.Issuer,
				Description = award.Description
			});
		}
	}

	private static void MapCourses(Person profile, DetailedPersonInformation info)
	{
		foreach (var course in info.AccomplishmentCourses)
		{
			profile.Courses.Add(new ProfileCourses
			{
				Name = course.Name,
				Number = course.Number
			});
		}
	}

	private static void MapProjects(Person profile, DetailedPersonInformation info)
	{
		foreach (var project in info.AccomplishmentProjects)
		{
			profile.Projects.Add(new ProfileProject
			{
				Title = project.Title,
				Description = project.Description
			});
		}
	}
}
