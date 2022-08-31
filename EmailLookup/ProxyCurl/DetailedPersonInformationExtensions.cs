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
			profile.Country = personInformation.CountryFullName;
			profile.Languages = personInformation.Languages;
			profile.PersonalEmails = personInformation.PersonalEmails;
			profile.PersonalNumbers = personInformation.PersonalNumbers;

			return profile;
		}
	}
}
