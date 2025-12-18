using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of noteworthy honors and awards this user has won
/// </summary>
[DataContract]
public class AccomplishmentHonorsAwards
{
	/// <summary>
	/// Title of honor/award
	/// </summary>
	[DataMember(Name = "title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Organisation body issuing this honor/award
	/// </summary>
	[DataMember(Name = "issuer")]
	public string Issuer { get; set; } = string.Empty;

	/// <summary>
	/// Date honor/award was issued on
	/// </summary>
	[DataMember(Name = "issued_on")]
	public ProxyCurlDate IssuedOn { get; set; } = new();

	/// <summary>
	/// Description
	/// </summary>
	[DataMember(Name = "description")]
	public string Description { get; set; } = string.Empty;
}