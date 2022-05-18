using System.Net.Mail;

namespace EmailLookup
{
   public class Person
   {
	  public Person(MailAddress mailAddress)
	  {
		 string user = mailAddress.User;
		 FirstName = user[..user.IndexOf(".")];
		 LastName = user[(user.IndexOf(".") + 1)..];
		 CompanyName = mailAddress.Host[..mailAddress.Host.IndexOf(".")];
		 Domain = mailAddress.Host;
	  }

	  public string FirstName { get; }

	  public string LastName { get; }

	  public string CompanyName { get; }

	  public string Domain { get; }
   }
}