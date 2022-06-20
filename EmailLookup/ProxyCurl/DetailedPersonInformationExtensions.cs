namespace EmailLookup.ProxyCurl
{
	internal static class DetailedPersonInformationExtensions
	{

		internal static Profile ToProfile(DetailedPersonInformation personInformation)
		{
			Profile profile = new Profile();

			profile.FirstName = personInformation.FirstName;
			profile.LastName = personInformation.LastName;

			return profile;
		}
	}
}
