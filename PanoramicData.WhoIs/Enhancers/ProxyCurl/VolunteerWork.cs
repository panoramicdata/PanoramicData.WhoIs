using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.ProxyCurl;

/// <summary>
/// List of historic work experiences
/// </summary>
[DataContract]
public class VolunteerWork
{
	/// <summary>
	/// Started at
	/// </summary>
	[DataMember(Name = "starts_at")]
	public ProxyCurlDate StartsAt { get; set; } = new();

	/// <summary>
	/// Ended at
	/// </summary>
	[DataMember(Name = "ends_at")]
	public ProxyCurlDate EndsAt { get; set; } = new();

	/// <summary>
	/// Name of volunteer activity
	/// </summary>
	[DataMember(Name = "title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Cause
	/// </summary>
	[DataMember(Name = "cause")]
	public string Cause { get; set; } = string.Empty;

	/// <summary>
	/// The company's display name
	/// </summary>
	[DataMember(Name = "company")]
	public string Company { get; set; } = string.Empty;

	/// <summary>
	/// The company's profile URL
	/// </summary>
	[DataMember(Name = "company_linkedin_profile_url")]
	public string CompanyLinkedinProfileUrl { get; set; } = string.Empty;

	/// <summary>
	/// Description
	/// </summary>
	[DataMember(Name = "description")]
	public string Description { get; set; } = string.Empty;
}