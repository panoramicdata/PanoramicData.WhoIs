using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
    /// <summary>
    /// List of noteworthy projects undertaken by this user
    /// </summary>
    [DataContract]
    public class AccomplishmentProjects
    {
        /// <summary>
        /// Start date of project
        /// </summary>
        [DataMember(Name = "starts_at")]
        public Date StartsAt { get; set; } = new();

        /// <summary>
        /// End date of project
        /// </summary>
        [DataMember(Name = "ends_at")]
        public Date EndsAt { get; set; } = new();

        /// <summary>
        /// Organisation body that issued the patent
        /// </summary>
        [DataMember(Name = "issuer")]
        public string Issuer { get; set; } = string.Empty;
    }
}