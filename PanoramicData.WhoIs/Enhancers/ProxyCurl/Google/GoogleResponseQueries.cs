namespace PanoramicData.WhoIs.Enhancers.ProxyCurl.Google;

/// <summary>
/// Contains the collection of request metadata included in the <c>queries</c> section
/// of a Google Custom Search API response.
/// </summary>
public class GoogleResponseQueries
  {
  /// <summary>
  /// List of requests used in search
  /// </summary>
  public List<GoogleQueriesRequest> Request { get; set; } = [];
   }