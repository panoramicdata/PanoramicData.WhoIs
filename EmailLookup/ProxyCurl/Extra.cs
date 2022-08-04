using System.Runtime.Serialization;

namespace EmailLookup.ProxyCurl
{
    /// <summary>
    /// Bundle of extra data on the user
    /// </summary>
    [DataContract]
    public class Extra
    {
        /// <summary>
        /// Profile's GitHub account
        /// </summary>
        [DataMember(Name = "github_profile_id")]
        public string GithubProfileId { get; set; } = string.Empty;

        /// <summary>
        /// Profile's Facebook account
        /// </summary>
        [DataMember(Name = "facebook_profile_id")]
        public string FacebookProfileId { get; set; } = string.Empty;

        /// <summary>
        /// Profile's Twitter account
        /// </summary>
        [DataMember(Name = "twitter_profile_id")]
        public string TwitterProfileId { get; set; } = string.Empty;
    }
}