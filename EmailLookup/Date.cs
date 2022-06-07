using System.Runtime.Serialization;

namespace EmailLookup
{
    /// <summary>
    /// Date
    /// </summary>
    [DataContract]
    public class Date
    {
        /// <summary>
        /// Day
        /// </summary>
        [DataMember(Name = "day")]
        public int Day { get; set; }

        /// <summary>
        /// Month
        /// </summary>
        [DataMember(Name = "month")]
        public int Month { get; set; }

        /// <summary>
        /// Year
        /// </summary>
        [DataMember(Name = "year")]
        public int Year { get; set; }
    }
}