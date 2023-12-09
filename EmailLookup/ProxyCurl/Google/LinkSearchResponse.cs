using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl.Google;

[DataContract]
    public class LinkSearchResponse
    {
        [DataMember(Name = "url")]
        public string Url { get; set; } = string.Empty;
    }
