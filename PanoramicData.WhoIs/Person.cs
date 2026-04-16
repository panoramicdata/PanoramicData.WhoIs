using PanoramicData.WhoIs.ProfileResult;
using System.Net.Mail;

namespace PanoramicData.WhoIs;

/// <summary>
/// Stores the basic information extracted from just the email address.
/// </summary>
/// <remarks>
/// Takes an email address and separates it into the User and Host, gets the company name
/// from the Host, and (if the email is in the format "firstname.surname@company.com")
/// gets the first and last name from the User.
/// </remarks>
public class Person
{
	/// <summary>
	/// The person's first (given) name.
	/// </summary>
	public string? FirstName { get; set; }

	/// <summary>
	/// The person's last (family) name.
	/// </summary>
	public string? LastName { get; set; }

	/// <summary>
	/// The company associated with this person, typically derived from the email domain.
	/// </summary>
	public Company? Company { get; set; }

	/// <summary>
	/// The parsed email address used as the primary input for enrichment.
	/// </summary>
	public MailAddress? MailAddress { get; set; }

	/// <summary>
	/// The person's gender, as reported by an enrichment source.
	/// </summary>
	public string? Gender { get; set; }

	/// <summary>
	/// The person's current job title or occupation.
	/// </summary>
	public string? Occupation { get; set; }

	/// <summary>
	/// The year the person was born, if known.
	/// </summary>
	public int? BirthYear { get; set; }

	/// <summary>
	/// The person's age in years, if known.
	/// </summary>
	public int? Age { get; set; }

	/// <summary>
	/// The country where the person is located, typically as a country name or ISO code.
	/// </summary>
	public string? Country { get; set; }

	/// <summary>
	/// The city where the person is located.
	/// </summary>
	public string? City { get; set; }

	/// <summary>
	/// The state or province where the person is located.
	/// </summary>
	public string? State { get; set; }

	/// <summary>
	/// The list of work experiences enriched from profile sources such as LinkedIn.
	/// </summary>
	public List<ProfileExperiences> Experiences { get; set; } = [];

	/// <summary>
	/// The list of educational qualifications enriched from profile sources such as LinkedIn.
	/// </summary>
	public List<ProfileEducation> Education { get; set; } = [];

	/// <summary>
	/// Languages spoken by the person, as reported by an enrichment source.
	/// </summary>
	public List<string> Languages { get; set; } = [];

	/// <summary>
	/// Personal (non-work) email addresses associated with the person.
	/// </summary>
	public List<string> PersonalEmails { get; set; } = [];

	/// <summary>
	/// Personal phone numbers associated with the person.
	/// </summary>
	public List<string> PersonalNumbers { get; set; } = [];

	/// <summary>
	/// The lower bound of the person's estimated salary range, as inferred by an enrichment source.
	/// </summary>
	public string? InferredSalaryMin { get; set; }

	/// <summary>
	/// The upper bound of the person's estimated salary range, as inferred by an enrichment source.
	/// </summary>
	public string? InferredSalaryMax { get; set; }

	/// <summary>
	/// Honours and awards received by the person, enriched from profile sources such as LinkedIn.
	/// </summary>
	public List<ProfileAwards> Awards { get; set; } = [];

	/// <summary>
	/// Courses completed by the person, enriched from profile sources such as LinkedIn.
	/// </summary>
	public List<ProfileCourses> Courses { get; set; } = [];

	/// <summary>
	/// Projects the person has worked on, enriched from profile sources such as LinkedIn.
	/// </summary>
	public List<ProfileProject> Projects { get; set; } = [];

	/// <summary>
	/// The URL of the person's LinkedIn profile, if found.
	/// </summary>
	public string? LinkedInUrl { get; set; }
}