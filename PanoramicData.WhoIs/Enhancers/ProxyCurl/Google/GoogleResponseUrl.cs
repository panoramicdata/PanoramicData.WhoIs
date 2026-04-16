namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Represents the URL template descriptor included in a Google Custom Search API response,
/// describing the parameterised URL that can be used to submit searches.
/// </summary>
public class GoogleResponseUrl
  {
  /// <summary>
  /// The MIME type of the URL template, typically <c>application/json</c>.
  /// </summary>
  public string Type { get; set; } = string.Empty;

  /// <summary>
  /// The URI template that represents the Google Custom Search endpoint pattern.
  /// </summary>
  public string Template { get; set; } = string.Empty;
   }
