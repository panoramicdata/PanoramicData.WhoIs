using System.Reflection;

namespace EmailLookup
{
	internal class ProfileMerger
	{

		internal static void Merge(Profile sourceProfile, Profile finalProfile)
		{
			if (string.IsNullOrEmpty(finalProfile.FirstName))
			{
				finalProfile.FirstName = sourceProfile.FirstName;
			}
			if (string.IsNullOrEmpty(finalProfile.LastName))
			{
				finalProfile.LastName = sourceProfile.LastName;
			}
			if (string.IsNullOrEmpty(finalProfile.Gender))
			{
				finalProfile.Gender = sourceProfile.Gender;
			}
			if (finalProfile.DayDOB == null || finalProfile.DayDOB == 0)
			{
				finalProfile.DayDOB = sourceProfile.DayDOB;
			}
			if (finalProfile.MonthDOB == null || finalProfile.MonthDOB == 0)
			{
				finalProfile.MonthDOB = sourceProfile.MonthDOB;
			}
			if (finalProfile.YearDOB == null || finalProfile.YearDOB == 0)
			{
				finalProfile.YearDOB = sourceProfile.YearDOB;
			}
			if (string.IsNullOrEmpty(finalProfile.Occupation))
			{
				finalProfile.Occupation = sourceProfile.Occupation;
			}
			if (string.IsNullOrEmpty(finalProfile.Country))
			{
				finalProfile.Country = sourceProfile.Country;
			}
			if (sourceProfile.Languages != null)
			{
				if (finalProfile.Languages == null)
				{
					finalProfile.Languages = sourceProfile.Languages;
				}
				else
				{
					foreach (var lang in sourceProfile.Languages)
					{
						if (!finalProfile.Languages.Contains(lang))
						{
							finalProfile.Languages.Add(lang);
						}
					}
				}
			}
			if (sourceProfile.PersonalEmails != null)
			{
				if (finalProfile.PersonalEmails == null)
				{
					finalProfile.PersonalEmails = sourceProfile.PersonalEmails;
				}
				else
				{
					foreach (var lang in sourceProfile.PersonalEmails)
					{
						if (!finalProfile.PersonalEmails.Contains(lang))
						{
							finalProfile.PersonalEmails.Add(lang);
						}
					}
				}
			}
			if (sourceProfile.PersonalNumbers != null)
			{
				if (finalProfile.PersonalNumbers == null)
				{
					finalProfile.PersonalNumbers = sourceProfile.PersonalNumbers;
				}
				else
				{
					foreach (var lang in sourceProfile.PersonalNumbers)
					{
						if (!finalProfile.PersonalNumbers.Contains(lang))
						{
							finalProfile.PersonalNumbers.Add(lang);
						}
					}
				}
			}
		}
	}
}