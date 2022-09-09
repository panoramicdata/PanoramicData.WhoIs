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
			if (string.IsNullOrEmpty(finalProfile.InferredSalaryMin))
			{
				finalProfile.InferredSalaryMin = sourceProfile.InferredSalaryMin;
			}
			if (string.IsNullOrEmpty(finalProfile.InferredSalaryMax))
			{
				finalProfile.InferredSalaryMax = sourceProfile.InferredSalaryMax;
			}
			if (sourceProfile.Languages is not null)
			{
				finalProfile.Languages = sourceProfile.Languages;
			}
			if (sourceProfile.PersonalEmails is not null)
			{
				finalProfile.PersonalEmails = sourceProfile.PersonalEmails;
			}
			if (sourceProfile.PersonalNumbers is not null)
			{
				finalProfile.PersonalNumbers = sourceProfile.PersonalNumbers;
			}
			if (sourceProfile.Awards is not null)
			{
				finalProfile.Awards = sourceProfile.Awards;
			}
			if (sourceProfile.Courses is not null)
			{
				finalProfile.Courses = sourceProfile.Courses;
			}
			if (sourceProfile.Projects is not null)
			{
				finalProfile.Projects = sourceProfile.Projects;
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