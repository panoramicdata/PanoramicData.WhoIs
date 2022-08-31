using Whois;

namespace EmailLookup.Core.ProxyCurl
{
	internal static class WhoisResponseExtensions
	{

		internal static Profile ToProfile(this WhoisResponse personInformation)
		{
			Profile profile = new Profile();

			profile.DomainName = personInformation.DomainName.Value;
			profile.RegistryDomainId = personInformation.RegistryDomainId;
			profile.RegistrarWhoIsServer = personInformation.WhoisServer.Value;
			profile.RegistrarUrl = personInformation.Registrar.Url;
			profile.UpdatedDate = personInformation.Updated;
			profile.CreationDate = personInformation.Registered;
			profile.RegistrarRegistrationExpirationDate = personInformation.Expiration;
			profile.Registrar = personInformation.Registrar.Name;
			profile.RegistrarIANAId = personInformation.Registrar.IanaId;
			profile.RegistrarAbuseContactEmail = personInformation.Registrar.AbuseEmail;
			profile.RegistrarAbuseContactPhone = personInformation.Registrar.AbuseTelephoneNumber;
			profile.DomainStatus = personInformation.DomainStatus[0];
			profile.RegistrantOrganization = personInformation.Registrant.Organization;
			profile.RegistrantState = personInformation.Registrant.Address[0];
			profile.RegistrantCountry = personInformation.Registrant.Address[1];
			profile.RegistrantEmail = personInformation.Registrant.Email;

			return profile;
		}
	}
}
