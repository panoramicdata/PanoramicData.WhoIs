using System.ComponentModel.DataAnnotations;

namespace EmailLookup.Demo.Web.Models
{
	public class LookupModel
	{
		[Required]
		public string EmailAddress { get; set; } = string.Empty;
	}
}
