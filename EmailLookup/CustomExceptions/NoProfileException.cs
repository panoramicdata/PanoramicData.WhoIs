using System.Runtime.Serialization;

namespace EmailLookup.CustomExceptions
{
	[Serializable]
	public class NoProfileException : Exception
	{
		public NoProfileException() { }
		public NoProfileException(string message) 
			: base(message)
		{ }

		protected NoProfileException(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			throw new NotImplementedException();
		}
	}
}