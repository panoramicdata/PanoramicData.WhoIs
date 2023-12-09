using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
    /// <summary>
    /// List of noteworthy patents won by this user
    /// </summary>
    [DataContract]
    public class AccomplishmentPatents
    {
        /// <summary>
        /// Title of patent
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Organisation body that issued the patent
        /// </summary>
        [DataMember(Name = "issuer")]
        public string Issuer { get; set; } = string.Empty;

        /// <summary>
        /// Date of patent issuance
        /// </summary>
        [DataMember(Name = "issued_on")]
        public ProxyCurlDate IssuedOn { get; set; } = new();

        /// <summary>
        /// Application number of the patent
        /// </summary>
        [DataMember(Name = "application_number")]
        public string ApplicationNumber { get; set; } = string.Empty;

        /// <summary>
        /// Numerical representation that identifies the patent
        /// </summary>
        [DataMember(Name = "patent_number")]
        public string PatentNumber { get; set; } = string.Empty;

        /// <summary>
        /// Url
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; } = string.Empty;
    }
}