using System.Net.Mail;

namespace EmailLookup.Core
{
	public class Person
	{
		public Person(string address)
		{
			MailAddressEmail = new MailAddress(address);
			string personName = MailAddressEmail.User;
			Email = address;
			LastName = personName;
			var separatorIndex = personName.IndexOf(".", StringComparison.Ordinal);
			if (separatorIndex > -1)
			{
				FirstName = personName[..personName.IndexOf(".", StringComparison.Ordinal)];
				LastName = personName[(personName.IndexOf(".", StringComparison.Ordinal) + 1)..];
			}
			CompanyName = MailAddressEmail.Host[..MailAddressEmail.Host.IndexOf(".", StringComparison.Ordinal)];
			Domain = MailAddressEmail.Host;
		}

		public string FirstName { get; } = string.Empty;
		public string LastName { get; }
		public string CompanyName { get; }
		public string Domain { get; }
		public string Email { get; }
		public MailAddress MailAddressEmail { get; }
	}
}