using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl;

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
        public string Min { get; set; } = string.Empty;

        /// <summary>
        /// Max
        /// </summary>
        [DataMember(Name = "max")]
        public string Max { get; set; } = string.Empty;
    }