using System.ComponentModel.DataAnnotations;

namespace BlogManagement.WebAPI.ViewModel
{
	public class AccountModel
	{
		[Required]
		public string Username { get; set; }
		[Required]
		public string Password { get; set; }
	}
}
