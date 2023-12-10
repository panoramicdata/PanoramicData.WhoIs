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
/// <param name="emailAddress">The email address that is to be manipulated</param>
public class Person
{
	public string? FirstName { get; set; }

	public string? LastName { get; set; }

	public Company? Company { get; set; }

	public MailAddress? MailAddress { get; set; }

	public string? Gender { get; set; }

	public string? Occupation { get; set; }

	public int? BirthYear { get; set; }

	public int? Age { get; set; }

	public string? Country { get; set; }

	public string? City { get; set; }

	public string? State { get; set; }

	public List<ProfileExperiences> Experiences { get; set; } = [];

	public List<ProfileEducation> Education { get; set; } = [];

	public List<string> Languages { get; set; } = [];

	public List<string> PersonalEmails { get; set; } = [];

	public List<string> PersonalNumbers { get; set; } = [];

	public string? InferredSalaryMin { get; set; }

	public string? InferredSalaryMax { get; set; }

	public List<ProfileAwards> Awards { get; set; } = [];

	public List<ProfileCourses> Courses { get; set; } = [];

	public List<ProfileProject> Projects { get; set; } = [];

	public string? LinkedInUrl { get; set; }
}