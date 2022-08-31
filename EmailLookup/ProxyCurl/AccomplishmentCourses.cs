using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
	/// <summary>
	/// List of noteworthy courses partook by this user
	/// </summary>
	[DataContract]
	public class AccomplishmentCourses
	{
		/// <summary>
		/// Name of course
		/// </summary>
		[DataMember(Name = "name")]
		public string Name { get; set; } = string.Empty;

		/// <summary>
		/// Numerical representation of the course
		/// </summary>
		[DataMember(Name = "number")]
		public string Number { get; set; } = string.Empty;
	}
}