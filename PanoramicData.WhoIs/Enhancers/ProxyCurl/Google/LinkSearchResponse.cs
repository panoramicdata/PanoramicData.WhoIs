using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.ProxyCurl.Google;

[DataContract]
public class LinkSearchResponse
{
	[DataMember(Name = "url")]
	public string Url { get; set; } = string.Empty;
}