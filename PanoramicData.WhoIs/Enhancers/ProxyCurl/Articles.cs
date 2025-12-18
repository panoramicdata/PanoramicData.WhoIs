using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// List of content-based articles posted by this user
/// </summary>
[DataContract]
public class Articles
{
	/// <summary>
	/// Title
	/// </summary>
	[DataMember(Name = "title")]
	public string Title { get; set; } = string.Empty;

	/// <summary>
	/// Link
	/// </summary>
	[DataMember(Name = "link")]
	public string Link { get; set; } = string.Empty;

	/// <summary>
	/// Date of publishing
	/// </summary>
	[DataMember(Name = "published_date")]
	public ProxyCurlDate PublishedDate { get; set; } = new();

	/// <summary>
	/// Author
	/// </summary>
	[DataMember(Name = "author")]
	public string Author { get; set; } = string.Empty;
}