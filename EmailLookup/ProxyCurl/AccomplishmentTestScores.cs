using System.Runtime.Serialization;

namespace EmailLookup
{
    /// <summary>
    /// List of noteworthy test scores accomplished by this user
    /// </summary>
    [DataContract]
    public class AccomplishmentTestScores
    {
        /// <summary>
        /// Title of course for which test score was derived from
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Test score
        /// </summary>
        [DataMember(Name = "score")]
        public string Score { get; set; } = string.Empty;

        /// <summary>
        /// Date test was assessed
        /// </summary>
        [DataMember(Name = "date_on")]
        public Date DateOn { get; set; } = new();

        /// <summary>
        /// Description of the test score
        /// </summary>
        [DataMember(Name = "description")]
        public string Description { get; set; } = string.Empty;
    }
}