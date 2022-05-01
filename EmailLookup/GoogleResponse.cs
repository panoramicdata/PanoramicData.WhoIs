using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace EmailLookup
{
    public class GoogleResponse
    {
        /// <summary>
        /// Type of search - should be customsearch#search
        /// </summary>
        public string Kind { get; set; } = string.Empty;

        public GoogleResponseUrl Url { get; set; } = new();

        /// <summary>
        /// Information on query used in search 
        /// </summary>
        public GoogleResponseQueries Queries { get; set; } = new();

        public List<GoogleResponseItems> Items { get; set; } = new();
    }
}
