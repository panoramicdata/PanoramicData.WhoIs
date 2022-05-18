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
	  }

	  public string FirstName { get; set; }

	  public string LastName { get; set; }

	  public string CompanyName { get; set; }
   }
}