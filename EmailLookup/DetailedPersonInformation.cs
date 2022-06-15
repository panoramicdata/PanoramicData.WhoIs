using System.Runtime.Serialization;

namespace EmailLookup
{
    /// <summary>
    /// JSON from Linkedin web scrape
    /// </summary>
    [DataContract]
    public class DetailedPersonInformation
    {
        /// <summary>
        /// The vanity identifier of the public LinkedIn profile
        /// </summary>
        [DataMember(Name = "public_identifier")]
        public string PublicIdentifier { get; set; } = string.Empty;

        /// <summary>
        /// First name of the user
        /// </summary>
        [DataMember(Name = "first_name")]
        public string FirstName { get; set; } = string.Empty;

        /// <summary>
        /// Last name of the user
        /// </summary>
        [DataMember(Name = "last_name")]
        public string LastName { get; set; } = string.Empty;

        /// <summary>
        /// Full name of the user
        /// </summary>
        [DataMember(Name = "full_name")]
        public string FullName { get; set; } = string.Empty;

        /// <summary>
        /// Title and company name of user's current employment
        /// </summary>
        [DataMember(Name = "occupation")]
        public string Occupation { get; set; } = string.Empty;

        /// <summary>
        /// Tagline written by user for their profile
        /// </summary>
        [DataMember(Name = "headline")]
        public string Headline { get; set; } = string.Empty;

        /// <summary>
        /// Blurb written by user for their profile
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// User's country of residence depicted by two letter code
        /// </summary>
        [DataMember(Name = "country")]
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// User's country of residence
        /// </summary>
        [DataMember(Name = "country_full_name")]
        public string CountryFullName { get; set; } = string.Empty;

        /// <summary>
        /// City user lives in
        /// </summary>
        [DataMember(Name = "city")]
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// State user lives in
        /// </summary>
        [DataMember(Name = "state")]
        public string State { get; set; } = string.Empty;

        /// <summary>
        /// List of user's historic work experience
        /// </summary>
        [DataMember(Name = "experiences")]
        public List<Experiences> Experiences { get; set; } = new();

        /// <summary>
        /// List of user's educational background
        /// </summary>
        [DataMember(Name = "education")]
        public List<Education> Education { get; set; } = new();

        /// <summary>
        /// List of languages user claims to be familiar with
        /// </summary>
        [DataMember(Name = "languages")]
        public List<string> Languages { get; set; } = new();

        /// <summary>
        /// List of noteworthy organizations that this user is part of
        /// </summary>
        [DataMember(Name = "accomplishment_organisations")]
        public List<AccomplishmentOrg> AccomplishmentOrganisations { get; set; } = new();

        /// <summary>
        /// List of noteworthy publications that this user has partook in
        /// </summary>
        [DataMember(Name = "accomplishment_publications")]
        public List<Publication> AccomplishmentPublications { get; set; } = new();

        /// <summary>
        /// List of noteworthy honors and awards this user has won
        /// </summary>
        [DataMember(Name = "accomplishment_honors_awards")]
        public List<AccomplishmentHonorsAwards> AccomplishmentHonorsAwards { get; set; } = new();

        /// <summary>
        /// List of noteworthy patents won by this user
        /// </summary>
        [DataMember(Name = "accomplishment_patents")]
        public List<AccomplishmentPatents> AccomplishmentPatents { get; set; } = new();

        /// <summary>
        /// List of noteworthy courses partook by this user
        /// </summary>
        [DataMember(Name = "accomplishment_courses")]
        public List<AccomplishmentCourses> AccomplishmentCourses { get; set; } = new();

        /// <summary>
        /// List of noteworthy projects undertaken by this user
        /// </summary>
        [DataMember(Name = "accomplishment_projects")]
        public List<AccomplishmentProjects> AccomplishmentProjects { get; set; } = new();

        /// <summary>
        /// List of noteworthy test scores accomplished by this user
        /// </summary>
        [DataMember(Name = "accomplishment_test_scores")]
        public List<AccomplishmentTestScores> AccomplishmentTestScores { get; set; } = new();

        /// <summary>
        /// List of historic work experiences
        /// </summary>
        [DataMember(Name = "volunteer_work")]
        public List<VolunteerWork> VolunteerWork { get; set; } = new();

        /// <summary>
        /// List of noteworthy certifications accomplished by this user
        /// </summary>
        [DataMember(Name = "certifications")]
        public List<Certifications> Certifications { get; set; } = new();

        /// <summary>
        /// Total count of LinkedIn connections
        /// </summary>
        [DataMember(Name = "connections")]
        public int Connections { get; set; }

        /// <summary>
        /// List of other LinkedIn profiles closely related to this user
        /// </summary>
        [DataMember(Name = "people_also_viewed")]
        public List<PeopleAlsoViewed> PeopleAlsoViewed { get; set; } = new();

        /// <summary>
        /// List of recommendations made by other users about this profile
        /// </summary>
        [DataMember(Name = "recommendations")]
        public List<string> Recommendations { get; set; } = new();

        /// <summary>
        /// List of content-based articles posted by this user
        /// </summary>
        [DataMember(Name = "articles")]
        public List<Articles> Articles { get; set; } = new();

        /// <summary>
        /// List of LinkedIn groups that this user is a part of
        /// </summary>
        [DataMember(Name = "groups")]
        public List<Groups> Groups { get; set; } = new();

        /// <summary>
        /// Salary range inferred from the user's current job title and company
        /// </summary>
        [DataMember(Name = "inferred_salary")]
        public InferredSalary InferredSalary { get; set; } = new();

        /// <summary>
        /// Gender of the user
        /// </summary>
        [DataMember(Name = "gender")]
        public string Gender { get; set; } = string.Empty;

        /// <summary>
        /// Birth date of the user
        /// </summary>
        [DataMember(Name = "birth_date")]
        public Date BirthDate { get; set; } = new();

        /// <summary>
        /// Industry that the user works in
        /// </summary>
        [DataMember(Name = "industry")]
        public string Industry { get; set; } = string.Empty;

        /// <summary>
        /// List of interests the user has
        /// </summary>
        [DataMember(Name = "interests")]
        public List<string> Interests { get; set; } = new();

        /// <summary>
        /// Bundle of extra data on the user
        /// </summary>
        [DataMember(Name = "extra")]
        public Extra Extra { get; set; } = new();

        /// <summary>
        /// List of personal emails associated with this user
        /// </summary>
        [DataMember(Name = "personal_emails")]
        public List<string> PersonalEmails { get; set; } = new();

        /// <summary>
        /// List of personal mobile phone numbers associated with this user
        /// </summary>
        [DataMember(Name = "personal_numbers")]
        public List<string> PersonalNumbers { get; set; } = new();
    }
}