using PanoramicData.WhoIs.ProfileResult;

namespace PanoramicData.WhoIs.ProxyCurl;

internal static class DetailedPersonInformationExtensions
{

	internal static Person ToProfile(this DetailedPersonInformation personInformation)
	{
		var profile = new Person
		{
			FirstName = personInformation.FirstName,
			LastName = personInformation.LastName,
			Gender = personInformation.Gender,
			Occupation = personInformation.Occupation
		};
		if (personInformation.BirthDate is not null)
		{
			profile.BirthYear = personInformation.BirthDate.Year;
			if (personInformation.BirthDate.Year != 0)
			{
				profile.Age = DateTime.Now.Year - personInformation.BirthDate.Year;
			}
		}

		profile.Country = personInformation.CountryFullName;
		profile.City = personInformation.City;
		profile.State = personInformation.State;
		profile.Languages = personInformation.Languages;
		profile.PersonalEmails = personInformation.PersonalEmails;
		profile.PersonalNumbers = personInformation.PersonalNumbers;
		foreach (var experience in personInformation.Experiences)
		{
			profile.Experiences.Add(new ProfileExperiences
			{
				Company = experience.Company,
				Title = experience.Title,
				Description = experience.Description,
				Location = experience.Location
			});
		}

		foreach (var education in personInformation.Education)
		{
			profile.Education.Add(new ProfileEducation
			{
				FieldOfStudy = education.FieldOfStudy,
				DegreeName = education.DegreeName,
				School = education.School,
				Description = education.Description
			});
		}

		foreach (var award in personInformation.AccomplishmentHonorsAwards)
		{
			profile.Awards.Add(new ProfileAwards
			{
				Title = award.Title,
				Issuer = award.Issuer,
				Description = award.Description
			});
		}

		foreach (var course in personInformation.AccomplishmentCourses)
		{
			profile.Courses.Add(new ProfileCourses
			{
				Name = course.Name,
				Number = course.Number
			});
		}

		foreach (var project in personInformation.AccomplishmentProjects)
		{
			profile.Projects.Add(new ProfileProject
			{
				Title = project.Title,
				Description = project.Description
			});
		}

		return profile;
	}
}
