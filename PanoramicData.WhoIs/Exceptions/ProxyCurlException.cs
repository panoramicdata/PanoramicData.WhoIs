using System.Runtime.Serialization;

namespace PanoramicData.WhoIs.Exceptions;

[Serializable]
public class ProxyCurlException : Exception
{
	public ProxyCurlException() { }
	public ProxyCurlException(string message)
		: base(message)
	{ }

	protected ProxyCurlException(SerializationInfo serializationInfo, StreamingContext streamingContext)
	{
		throw new NotImplementedException();
	}
}
