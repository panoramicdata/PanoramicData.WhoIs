namespace EmailLookup
{
	public class GoogleSearchResponse
	{
		public string Title { get; set; } = string.Empty;

		public string Url { get; set; } = string.Empty;

		public string Description { get; set; } = string.Empty;

		public int Score { get; set; }
	}
}
