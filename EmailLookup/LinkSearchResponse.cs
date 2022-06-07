using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmailLookup
{
    [DataContract]
    public class LinkSearchResponse
    {
        [DataMember(Name = "url")]
        public string Url { get; set; } = string.Empty;
    }
}
