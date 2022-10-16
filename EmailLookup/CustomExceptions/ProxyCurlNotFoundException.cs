using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace EmailLookup.CustomExceptions
{
	[Serializable]
	public class ProxyCurlNotFoundException : Exception
	{
		public ProxyCurlNotFoundException() { }
		public ProxyCurlNotFoundException(string message)
			: base(message)
		{ }

		protected ProxyCurlNotFoundException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}
