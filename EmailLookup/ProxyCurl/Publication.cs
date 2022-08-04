using System.Runtime.Serialization;

namespace EmailLookup.ProxyCurl
{
    /// <summary>
    /// List of noteworthy publications that this user has partook in
    /// </summary>
    [DataContract]
    public class Publication
    {
        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Publishing organisation body
        /// </summary>
        [DataMember(Name = "publisher")]
        public string Publisher { get; set; } = string.Empty;

        /// <summary>
        /// Date of publication
        /// </summary>
        [DataMember(Name = "published_on")]
        public Date PublishedOn { get; set; } = new();

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// URL of publication
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; } = string.Empty;
    }
}