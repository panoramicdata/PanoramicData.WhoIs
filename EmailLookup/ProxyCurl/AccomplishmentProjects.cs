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
        public ProxyCurlDate StartsAt { get; set; } = new();

        /// <summary>
        /// End date of project
        /// </summary>
        [DataMember(Name = "ends_at")]
        public ProxyCurlDate EndsAt { get; set; } = new();

		/// <summary>
		/// Name of the project that has been or is currently being worked on.
		/// </summary>
		[DataMember(Name = "title")]
        public string Title { get; set; } = string.Empty;

		/// <summary>
		/// Description of the project.
		/// </summary>
		[DataMember(Name = "description")]
		public string Description { get; set; } = string.Empty;
	}
}