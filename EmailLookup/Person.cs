using System.Net.Mail;

namespace EmailLookup
{
   public class Person
   {
	  public Person(string address)
	  {
		 MailAddressEmail = new MailAddress(address);
		 string user = MailAddressEmail.User;
		 Email = address;
		 FirstName = user[..user.IndexOf(".", StringComparison.Ordinal)];
		 LastName = user[(user.IndexOf(".", StringComparison.Ordinal) + 1)..];
		 CompanyName = MailAddressEmail.Host[..MailAddressEmail.Host.IndexOf(".", StringComparison.Ordinal)];
		 Domain = MailAddressEmail.Host;
	  }

	  public string FirstName { get; }

	  public string LastName { get; }

	  public string CompanyName { get; }

	  public string Domain { get; }

      public string Email { get; }

	  public MailAddress MailAddressEmail { get; }
   }
}