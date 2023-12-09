using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
    /// <summary>
    /// List of user's historic work experience
    /// </summary>
    [DataContract]
    public class Experiences
    {
        /// <summary>
        /// Start date of the work experience
        /// </summary>
        [DataMember(Name = "starts_at")]
        public ProxyCurlDate StartsAt { get; set; } = new();

        /// <summary>
        /// End date of the work experience
        /// </summary>
        [DataMember(Name = "ends_at")]
        public ProxyCurlDate EndsAt { get; set; } = new();

        /// <summary>
        /// Company's display name
        /// </summary>
        [DataMember(Name = "company")]
        public string Company { get; set; } = string.Empty;

        /// <summary>
        /// Company's profile URL
        /// </summary>
        [DataMember(Name = "company_linkedin_profile_url")]
        public string CompanyLinkedinProfileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Title
        /// </summary>
        [DataMember(Name = "title")]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Location
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; } = string.Empty;
    }
}