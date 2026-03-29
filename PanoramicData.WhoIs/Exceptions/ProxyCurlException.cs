namespace PanoramicData.WhoIs.Exceptions;

[Serializable]
public class ProxyCurlException : Exception
{
	public ProxyCurlException(string message)
		: base(message)
	{ }
}
