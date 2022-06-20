namespace EmailLookup
{
	internal class ProfileMerger
	{

		internal void Merge(Profile sourceProfile, Profile finalProfile)
		{
			if (string.IsNullOrEmpty(finalProfile.FirstName))
			{
				finalProfile.FirstName = sourceProfile.FirstName;
			}
		}
	}
}
