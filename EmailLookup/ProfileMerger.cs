using System.Reflection;

namespace EmailLookup.Core
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
			if (string.IsNullOrEmpty(finalProfile.DomainName))
			{
				finalProfile.DomainName = sourceProfile.DomainName;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistryDomainId))
			{
				finalProfile.RegistryDomainId = sourceProfile.RegistryDomainId;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrarWhoIsServer))
			{
				finalProfile.RegistrarWhoIsServer = sourceProfile.RegistrarWhoIsServer;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrarUrl))
			{
				finalProfile.RegistrarUrl = sourceProfile.RegistrarUrl;
			}
			if (finalProfile.UpdatedDate is null)
			{
				finalProfile.UpdatedDate = sourceProfile.UpdatedDate;
			}
			if (finalProfile.CreationDate is null)
			{
				finalProfile.CreationDate = sourceProfile.CreationDate;
			}
			if (finalProfile.RegistrarRegistrationExpirationDate is null)
			{
				finalProfile.RegistrarRegistrationExpirationDate = sourceProfile.RegistrarRegistrationExpirationDate;
			}
			if (string.IsNullOrEmpty(finalProfile.Registrar))
			{
				finalProfile.Registrar = sourceProfile.Registrar;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrarIANAId))
			{
				finalProfile.RegistrarIANAId = sourceProfile.RegistrarIANAId;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrarAbuseContactEmail))
			{
				finalProfile.RegistrarAbuseContactEmail = sourceProfile.RegistrarAbuseContactEmail;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrarAbuseContactPhone))
			{
				finalProfile.RegistrarAbuseContactPhone = sourceProfile.RegistrarAbuseContactPhone;
			}
			if (string.IsNullOrEmpty(finalProfile.DomainStatus))
			{
				finalProfile.DomainStatus = sourceProfile.DomainStatus;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrantOrganization))
			{
				finalProfile.RegistrantOrganization = sourceProfile.RegistrantOrganization;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrantState))
			{
				finalProfile.RegistrantState = sourceProfile.RegistrantState;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrantCountry))
			{
				finalProfile.RegistrantCountry = sourceProfile.RegistrantCountry;
			}
			if (string.IsNullOrEmpty(finalProfile.RegistrantEmail))
			{
				finalProfile.RegistrantEmail = sourceProfile.RegistrantEmail;
			}
		}
	}
}