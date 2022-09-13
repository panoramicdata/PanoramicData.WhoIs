namespace EmailLookup.Core.ProxyCurl
{
	internal static class DetailedPersonInformationExtensions
	{

		internal static Profile ToProfile(this DetailedPersonInformation personInformation)
		{
			Profile profile = new Profile();

			profile.FirstName = personInformation.FirstName;
			profile.LastName = personInformation.LastName;
			profile.Gender = personInformation.Gender;
			profile.Occupation = personInformation.Occupation;
			if (personInformation.BirthDate is not null)
			{
				profile.BirthYear = personInformation.BirthDate.Year;
				if (personInformation.BirthDate.Year != 0)
				{
					profile.Age = DateTime.Now.Year - personInformation.BirthDate.Year;
				}
			}
			profile.Country = personInformation.CountryFullName;
			profile.Languages = personInformation.Languages;
			profile.PersonalEmails = personInformation.PersonalEmails;
			profile.PersonalNumbers = personInformation.PersonalNumbers;

			return profile;
		}
	}
}
