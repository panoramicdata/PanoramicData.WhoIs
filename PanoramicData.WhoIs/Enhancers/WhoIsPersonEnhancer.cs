using Whois;

namespace PanoramicData.WhoIs.Enhancers;

/// <summary>
/// An interface that interacts with the WHOIS Lookup API, which returns detailed information
/// on a company's domain information.
/// </summary>
public class WhoIsPersonEnhancer : BasicPersonEnhancer
{
	/// <summary>
	/// Searches the WHOIS database for information on a domain and stores that information in a
	/// Profile object, which is then returned.
	/// </summary>
	public override async Task<Person> EnhanceAsync(Person person, CancellationToken cancellationToken)
	{
		person = BasicEnhance(person);

		if (person.Company is null || string.IsNullOrWhiteSpace(person.Company.DomainName))
		{
			return person;
		}

		var domain = person.Company.DomainName;

		var response = await new WhoisLookup()
			.LookupAsync(domain)
			.ConfigureAwait(false);

		if (response is null)
		{
			return person;
		}

		return Merge(
			person,
			new Person
			{
				Company = new Company
				{
					Name = response.AdminContact?.Organization,
					AdminEmail = response.AdminContact?.Email,
					DomainName = response.DomainName.Value,
					RegistryDomainId = response.RegistryDomainId,
					RegistrarWhoIsServer = response.WhoisServer?.Value,
					RegistrarUrl = response.Registrar?.Url,
					UpdatedDate = response.Updated,
					CreationDate = response.Registered,
					RegistrarRegistrationExpirationDate = response.Expiration,
					Registrar = response.Registrar?.Name,
					RegistrarIanaId = response.Registrar?.IanaId,
					RegistrarAbuseContactEmail = response.Registrar?.AbuseEmail,
					RegistrarAbuseContactPhone = response.Registrar?.AbuseTelephoneNumber,
					DomainStatus = response.DomainStatus?.FirstOrDefault(),
					RegistrantOrganization = response.Registrant?.Organization,
					RegistrantState = response.Registrant?.Address?.FirstOrDefault(),
					RegistrantCountry = response.Registrant?.Address?.FirstOrDefault(),
					RegistrantEmail = response.Registrant?.Email,
				},
			}
		);
	}
}
