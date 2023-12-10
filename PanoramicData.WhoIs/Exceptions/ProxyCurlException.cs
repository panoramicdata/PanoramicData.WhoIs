using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PanoramicData.WhoIs.CustomExceptions;

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
