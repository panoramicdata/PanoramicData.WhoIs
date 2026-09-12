using System.Text.Json.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// JSON from Linkedin web scrape
/// </summary>
public class DetailedPersonInformation
{
	/// <summary>
	/// The vanity identifier of the public LinkedIn profile
	/// </summary>
	[JsonPropertyName("public_identifier")]
	public string PublicIdentifier { get; set; } = string.Empty;

	/// <summary>
	/// First name of the user
	/// </summary>
	[JsonPropertyName("first_name")]
	public string FirstName { get; set; } = string.Empty;

	/// <summary>
	/// Last name of the user
	/// </summary>
	[JsonPropertyName("last_name")]
	public string LastName { get; set; } = string.Empty;

	/// <summary>
	/// Full name of the user
	/// </summary>
	[JsonPropertyName("full_name")]
	public string FullName { get; set; } = string.Empty;

	/// <summary>
	/// Title and company name of user's current employment
	/// </summary>
	[JsonPropertyName("occupation")]
	public string Occupation { get; set; } = string.Empty;

	/// <summary>
	/// Tagline written by user for their profile
	/// </summary>
	[JsonPropertyName("headline")]
	public string Headline { get; set; } = string.Empty;

	/// <summary>
	/// Blurb written by user for their profile
	/// </summary>
	[JsonPropertyName("summary")]
	public string Summary { get; set; } = string.Empty;

	/// <summary>
	/// User's country of residence depicted by two letter code
	/// </summary>
	[JsonPropertyName("country")]
	public string Country { get; set; } = string.Empty;

	/// <summary>
	/// User's country of residence
	/// </summary>
	[JsonPropertyName("country_full_name")]
	public string CountryFullName { get; set; } = string.Empty;

	/// <summary>
	/// City user lives in
	/// </summary>
	[JsonPropertyName("city")]
	public string City { get; set; } = string.Empty;

	/// <summary>
	/// State user lives in
	/// </summary>
	[JsonPropertyName("state")]
	public string State { get; set; } = string.Empty;

	/// <summary>
	/// List of user's historic work experience
	/// </summary>
	[JsonPropertyName("experiences")]
	public List<Experiences> Experiences { get; set; } = [];

	/// <summary>
	/// List of user's educational background
	/// </summary>
	[JsonPropertyName("education")]
	public List<Education> Education { get; set; } = [];

	/// <summary>
	/// List of languages user claims to be familiar with
	/// </summary>
	[JsonPropertyName("languages")]
	public List<string> Languages { get; set; } = [];

	/// <summary>
	/// List of noteworthy organizations that this user is part of
	/// </summary>
	[JsonPropertyName("accomplishment_organisations")]
	public List<AccomplishmentOrg> AccomplishmentOrganizations { get; set; } = [];

	/// <summary>
	/// List of noteworthy publications that this user has partook in
	/// </summary>
	[JsonPropertyName("accomplishment_publications")]
	public List<Publication> AccomplishmentPublications { get; set; } = [];

	/// <summary>
	/// List of noteworthy honors and awards this user has won
	/// </summary>
	[JsonPropertyName("accomplishment_honors_awards")]
	public List<AccomplishmentHonorsAwards> AccomplishmentHonorsAwards { get; set; } = [];

	/// <summary>
	/// List of noteworthy patents won by this user
	/// </summary>
	[JsonPropertyName("accomplishment_patents")]
	public List<AccomplishmentPatents> AccomplishmentPatents { get; set; } = [];

	/// <summary>
	/// List of noteworthy courses partook by this user
	/// </summary>
	[JsonPropertyName("accomplishment_courses")]
	public List<AccomplishmentCourses> AccomplishmentCourses { get; set; } = [];

	/// <summary>
	/// List of noteworthy projects undertaken by this user
	/// </summary>
	[JsonPropertyName("accomplishment_projects")]
	public List<AccomplishmentProjects> AccomplishmentProjects { get; set; } = [];

	/// <summary>
	/// List of noteworthy test scores accomplished by this user
	/// </summary>
	[JsonPropertyName("accomplishment_test_scores")]
	public List<AccomplishmentTestScores> AccomplishmentTestScores { get; set; } = [];

	/// <summary>
	/// List of historic work experiences
	/// </summary>
	[JsonPropertyName("volunteer_work")]
	public List<VolunteerWork> VolunteerWork { get; set; } = [];

	/// <summary>
	/// List of noteworthy certifications accomplished by this user
	/// </summary>
	[JsonPropertyName("certifications")]
	public List<Certifications> Certifications { get; set; } = [];

	/// <summary>
	/// Total count of LinkedIn connections
	/// </summary>
	[JsonPropertyName("connections")]
	public int? Connections { get; set; }

	/// <summary>
	/// List of other LinkedIn profiles closely related to this user
	/// </summary>
	[JsonPropertyName("people_also_viewed")]
	public List<PeopleAlsoViewed> PeopleAlsoViewed { get; set; } = [];

	/// <summary>
	/// List of recommendations made by other users about this profile
	/// </summary>
	[JsonPropertyName("recommendations")]
	public List<string> Recommendations { get; set; } = [];

	/// <summary>
	/// List of content-based articles posted by this user
	/// </summary>
	[JsonPropertyName("articles")]
	public List<Articles> Articles { get; set; } = [];

	/// <summary>
	/// List of LinkedIn groups that this user is a part of
	/// </summary>
	[JsonPropertyName("groups")]
	public List<Groups> Groups { get; set; } = [];

	/// <summary>
	/// Salary range inferred from the user's current job title and company
	/// </summary>
	[JsonPropertyName("inferred_salary")]
	public InferredSalary InferredSalary { get; set; } = new();

	/// <summary>
	/// Gender of the user
	/// </summary>
	[JsonPropertyName("gender")]
	public string Gender { get; set; } = string.Empty;

	/// <summary>
	/// Birth date of the user
	/// </summary>
	[JsonPropertyName("birth_date")]
	public ProxyCurlDate BirthDate { get; set; } = new();

	/// <summary>
	/// Industry that the user works in
	/// </summary>
	[JsonPropertyName("industry")]
	public string Industry { get; set; } = string.Empty;

	/// <summary>
	/// List of interests the user has
	/// </summary>
	[JsonPropertyName("interests")]
	public List<string> Interests { get; set; } = [];

	/// <summary>
	/// Bundle of extra data on the user
	/// </summary>
	[JsonPropertyName("extra")]
	public Extra Extra { get; set; } = new();

	/// <summary>
	/// List of personal emails associated with this user
	/// </summary>
	[JsonPropertyName("personal_emails")]
	public List<string> PersonalEmails { get; set; } = [];

	/// <summary>
	/// List of personal mobile phone numbers associated with this user
	/// </summary>
	[JsonPropertyName("personal_numbers")]
	public List<string> PersonalNumbers { get; set; } = [];
}