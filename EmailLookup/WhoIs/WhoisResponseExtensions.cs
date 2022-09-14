using Whois;

namespace EmailLookup.Core.ProxyCurl
{
	internal static class WhoisResponseExtensions
	{

		internal static Profile ToProfile(this WhoisResponse personInformation)
		{
			var profile = new Profile
			{
				DomainName = personInformation.DomainName?.Value ?? string.Empty,
				RegistryDomainId = personInformation.RegistryDomainId,
				RegistrarWhoIsServer = personInformation.WhoisServer.Value,
				RegistrarUrl = personInformation.Registrar.Url,
				UpdatedDate = personInformation.Updated,
				CreationDate = personInformation.Registered,
				RegistrarRegistrationExpirationDate = personInformation.Expiration,
				Registrar = personInformation.Registrar.Name,
				RegistrarIANAId = personInformation.Registrar.IanaId,
				RegistrarAbuseContactEmail = personInformation.Registrar.AbuseEmail,
				RegistrarAbuseContactPhone = personInformation.Registrar.AbuseTelephoneNumber,
				DomainStatus = personInformation.DomainStatus[0],
				RegistrantOrganization = personInformation.Registrant.Organization,
				RegistrantState = personInformation.Registrant.Address[0],
				RegistrantCountry = personInformation.Registrant.Address[1],
				RegistrantEmail = personInformation.Registrant.Email
			};

			return profile;
		}
	}
}
