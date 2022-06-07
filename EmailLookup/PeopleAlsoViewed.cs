using System.Runtime.Serialization;

namespace EmailLookup
{
    /// <summary>
    /// A list of other LinkedIn profiles closely related to this user
    /// </summary>
    [DataContract]
    public class PeopleAlsoViewed
    {
        /// <summary>
        /// URL of the profile
        /// </summary>
        [DataMember(Name = "link")]
        public string Link { get; set; } = string.Empty;

        /// <summary>
        /// Name
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Summary
        /// </summary>
        [DataMember(Name = "summary")]
        public string Summary { get; set; } = string.Empty;

        /// <summary>
        /// Location
        /// </summary>
        [DataMember(Name = "location")]
        public string Location { get; set; } = string.Empty;
    }
}