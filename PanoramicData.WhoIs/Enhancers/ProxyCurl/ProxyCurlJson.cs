using System.Text.Json;

namespace PanoramicData.WhoIs.Enhancers.ProxyCurl;

/// <summary>
/// Shared <see cref="JsonSerializerOptions"/> for ProxyCurl and Google Custom Search payloads.
/// </summary>
internal static class ProxyCurlJson
{
	/// <summary>
	/// Options matching the defaults used by <c>HttpClient.GetFromJsonAsync</c>, so that
	/// responses read from a cache file deserialize identically to responses read from the wire.
	/// </summary>
	internal static readonly JsonSerializerOptions Options = new(JsonSerializerDefaults.Web);
}
