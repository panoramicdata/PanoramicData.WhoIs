namespace EmailLookup
{
	public class Profile
	{
		public LookupOutcomes Outcome { get; set; }

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public string? Occupation { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public List<string>? Languages { get; set; }
		public List<string>? PersonalEmails { get; set; }
		public List<string>? PersonalNumbers { get; set; }
		public string? DomainName { get; set; }
		public string? RegistryDomainId { get; set; }
		public string? RegistrarWhoIsServer { get; set; }
		public string? RegistrarUrl { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public DateTime? CreationDate { get; set; }
		public DateTime? RegistrarRegistrationExpirationDate { get; set; }
		public string? Registrar { get; set; }
		public string? RegistrarIANAId { get; set; }
		public string? RegistrarAbuseContactEmail { get; set; }
		public string? RegistrarAbuseContactPhone { get; set; }
		public string? DomainStatus { get; set; }
		public string? RegistrantOrganization { get; set; }
		public string? RegistrantState { get; set; }
		public string? RegistrantCountry { get; set; }
		public string? RegistrantEmail { get; set; }
	}
}
