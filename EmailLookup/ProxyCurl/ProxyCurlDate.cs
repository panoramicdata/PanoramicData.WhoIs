using System.Runtime.Serialization;

namespace EmailLookup.Core.ProxyCurl
{
    /// <summary>
    /// Date
    /// </summary>
    [DataContract]
    public class ProxyCurlDate
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