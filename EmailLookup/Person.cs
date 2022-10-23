using System.Net.Mail;

namespace EmailLookup.Core
{
	/// <summary>
	/// Stores the basic information extracted from just the email address.
	/// </summary>
	public class Person
	{
		/// <summary>
		/// Takes an email address and seperates it into the User and Host, gets the company name
		/// from the Host, and (if the email is in the format "firstname.surname@company.com")
		/// gets the first and last name from the User.
		/// </summary>
		/// <param name="address"></param>
		public Person(string address)
		{
			MailAddressEmail = new MailAddress(address);
			var personName = MailAddressEmail.User;
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