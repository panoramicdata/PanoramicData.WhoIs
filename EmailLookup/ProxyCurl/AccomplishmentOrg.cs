using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
    /// <summary>
    /// List of noteworthy organizations that this user is part of
    /// </summary>
    [DataContract]
    public class AccomplishmentOrg
    {
        /// <summary>
        /// Start date
        /// </summary>
        [DataMember(Name = "starts_at")]
        public Date StartsAt { get; set; } = new();

        /// <summary>
        /// End date
        /// </summary>
        [DataMember(Name = "ends_at")]
        public Date EndsAt { get; set; } = new();

        /// <summary>
        /// Organisation name
        /// </summary>
        [DataMember(Name = "org_name")]
        public string OrgName { get; set; } = string.Empty;

        /// <summary>
        /// Title at organisation
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;
    }
}