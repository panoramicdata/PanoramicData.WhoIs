namespace EmailLookup.Core
{
	public class Profile
	{
		public LookupOutcomes Outcome { get; set; }
		public string FirstName { get; set; } = string.Empty;
		public string LastName { get; set; } = string.Empty;
		public string Gender { get; set; } = string.Empty;
		public string Occupation { get; set; } = string.Empty;
		public int BirthYear { get; set; }
		public int Age { get; set; }
		public string Country { get; set; } = string.Empty;
		public string City { get; set; } = string.Empty;
		public string State { get; set; } = string.Empty;
		public List<ProfileExperiences> Experiences { get; set; } = new List<ProfileExperiences>();
		public List<ProfileEducation> Education { get; set; } = new List<ProfileEducation>();
		public List<string> Languages { get; set; } = new List<string>();
		public List<string> PersonalEmails { get; set; } = new List<string>();
		public List<string> PersonalNumbers { get; set; } = new List<string>();
		public string InferredSalaryMin { get; set; } = string.Empty;
		public string InferredSalaryMax { get; set; } = string.Empty;
		public List<ProfileAwards> Awards { get; set; } = new List<ProfileAwards>();
		public List<ProfileCourses> Courses { get; set; } = new List<ProfileCourses>();
		public List<ProfileProject> Projects { get; set; } = new List<ProfileProject>();
		public string DomainName { get; set; } = string.Empty;
		public string RegistryDomainId { get; set; } = string.Empty;
		public string RegistrarWhoIsServer { get; set; } = string.Empty;
		public string RegistrarUrl { get; set; } = string.Empty;
		public DateTime? UpdatedDate { get; set; }
		public DateTime? CreationDate { get; set; }
		public DateTime? RegistrarRegistrationExpirationDate { get; set; }
		public string Registrar { get; set; } = string.Empty;
		public string RegistrarIANAId { get; set; } = string.Empty;
		public string RegistrarAbuseContactEmail { get; set; } = string.Empty;
		public string RegistrarAbuseContactPhone { get; set; } = string.Empty;
		public string DomainStatus { get; set; } = string.Empty;
		public string RegistrantOrganization { get; set; } = string.Empty;
		public string RegistrantState { get; set; } = string.Empty;
		public string RegistrantCountry { get; set; } = string.Empty;
		public string RegistrantEmail { get; set; } = string.Empty;
	}
}
