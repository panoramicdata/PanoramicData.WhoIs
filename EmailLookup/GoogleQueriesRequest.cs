using System.Runtime.Serialization;

namespace EmailLookup
{
    public class GoogleQueriesRequest
    {
        public string Title { get; set; } = string.Empty;

        public int Count { get; set; }

        public int StartIndex { get; set; }


    }
}