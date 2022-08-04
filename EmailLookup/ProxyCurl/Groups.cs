using System.Runtime.Serialization;

namespace EmailLookup.ProxyCurl
{
    /// <summary>
    /// List of LinkedIn groups that this user is a part of
    /// </summary>
    [DataContract]
    public class Groups
    {
        /// <summary>
        /// Name of the group
        /// </summary>
        [DataMember(Name = "name")]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// URL to LinkedIn group
        /// </summary>
        [DataMember(Name = "url")]
        public string Url { get; set; } = string.Empty;
    }
}