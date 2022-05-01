using Newtonsoft.Json;

namespace EmailLookup
{
    public class PageMapMetatags
    {
        [JsonProperty("og:description")]
        public string OgDesc { get; set; } = string.Empty;

        [JsonProperty("og:url")]
        public string OgUrl { get; set; } = string.Empty;

        [JsonProperty("og:title")]
        public string OgTitle { get; set; } = string.Empty;
    }
}