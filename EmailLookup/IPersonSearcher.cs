namespace EmailLookup.Core
{
	public interface IPersonSearcher
	{
		Task<Profile?> SearchAsync(Person person);

	}
}
