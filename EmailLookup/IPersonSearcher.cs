namespace EmailLookup
{
	public interface IPersonSearcher
	{
		Task<Profile?> SearchAsync(Person person);

	}
}
