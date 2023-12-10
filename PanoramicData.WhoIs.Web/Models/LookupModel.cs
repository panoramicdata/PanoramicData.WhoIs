using System.ComponentModel.DataAnnotations;

namespace PanoramicData.WhoIs.Web.Models;

public class LookupModel
{
	[Required]
	public string EmailAddress { get; set; } = string.Empty;
}
