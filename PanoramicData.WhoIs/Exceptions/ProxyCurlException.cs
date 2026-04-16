namespace PanoramicData.WhoIs.Exceptions;

/// <summary>
/// The exception thrown when the ProxyCurl API returns an unexpected response
/// or when a required operation using the ProxyCurl service fails.
/// </summary>
[Serializable]
public class ProxyCurlException : Exception
{
	/// <summary>
	/// Initialises a new instance of <see cref="ProxyCurlException"/> with the specified error message.
	/// </summary>
	/// <param name="message">A message that describes the error.</param>
	public ProxyCurlException(string message)
		: base(message)
	{ }
}
