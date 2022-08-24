namespace EmailLookup
{
	internal interface IPersonSearcher
	{
		Task<Profile?> SearchAsync(Person person);

	}
}
