namespace EmailLookup.ProxyCurl.Google
{
    public class GoogleResponseItems
    {
        public string Kind { get; set; } = string.Empty;

        public string Title { get; set; } = string.Empty;

        public string Link { get; set; } = string.Empty;

        public string Snippet { get; set; } = string.Empty;

        public ResponseItemPageMap PageMap { get; set; } = new();
    }
}