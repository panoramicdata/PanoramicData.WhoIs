using System.Runtime.Serialization;

namespace EmailLookup
{
    /// <summary>
    /// Salary range inferred from the user's current job title and company
    /// </summary>
    [DataContract]
    public class InferredSalary
    {
        /// <summary>
        /// Min
        /// </summary>
        [DataMember(Name = "min")]
        public int Min { get; set; }

        /// <summary>
        /// Max
        /// </summary>
        [DataMember(Name = "max")]
        public int Max { get; set; }
    }
}