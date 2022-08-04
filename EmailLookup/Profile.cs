namespace EmailLookup
{
	public class Profile
	{
		public LookupOutcomes Outcome { get; set; }

		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Gender { get; set; }
		public int? DayDOB { get; set; }
		public int? MonthDOB { get; set; }
		public int? YearDOB { get; set; }
		public string? Occupation { get; set; }
		public string? Country { get; set; }
		public string? City { get; set; }
		public string? State { get; set; }
		public List<string>? Languages { get; set; }
		public List<string>? PersonalEmails { get; set; }
		public List<string>? PersonalNumbers { get; set; }
	}
}
