using System.Runtime.Serialization;

namespace EmailLookup.ProxyCurl
{
    /// <summary>
    /// List of user's educational background
    /// </summary>
    [DataContract]
    public class Education
    {
        /// <summary>
        /// Start date of the education
        /// </summary>
        [DataMember(Name = "starts_at")]
        public Date StartsAt { get; set; } = new();

        /// <summary>
        /// End date of the education
        /// </summary>
        [DataMember(Name = "ends_at")]
        public Date EndsAt { get; set; } = new();

        /// <summary>
        /// Field of study
        /// </summary>
        [DataMember(Name = "field_of_study")]
        public string FieldOfStudy { get; set; } = string.Empty;

        /// <summary>
        /// Degree name
        /// </summary>
        [DataMember(Name = "degree_name")]
        public string DegreeName { get; set; } = string.Empty;

        /// <summary>
        /// School
        /// </summary>
        [DataMember(Name = "school")]
        public string School { get; set; } = string.Empty;

        /// <summary>
        /// School profile URL
        /// </summary>
        [DataMember(Name = "school_linkedin_profile_url")]
        public string SchoolLinkedinProfileUrl { get; set; } = string.Empty;

        /// <summary>
        /// Description
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;
    }
}