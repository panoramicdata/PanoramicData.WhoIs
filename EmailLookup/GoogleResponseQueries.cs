using System.Runtime.Serialization;

namespace EmailLookup
{
    public class GoogleResponseQueries
    {
        /// <summary>
        /// List of requests used in search
        /// </summary>
        public List<GoogleQueriesRequest> Request { get; set; } = new();
    }
}