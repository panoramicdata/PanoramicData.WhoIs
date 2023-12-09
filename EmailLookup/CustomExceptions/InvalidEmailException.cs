using System.Runtime.Serialization;

namespace EmailLookup.CustomExceptions;

[Serializable]
public class InvalidEmailException : Exception
{
	public InvalidEmailException() { }
	public InvalidEmailException(string email) 
		: base($"{email} is not a valid email address.")
	{ }

	protected InvalidEmailException(SerializationInfo serializationInfo, StreamingContext streamingContext)
	{
		throw new NotImplementedException();
	}
}