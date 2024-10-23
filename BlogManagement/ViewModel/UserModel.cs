using System.ComponentModel.DataAnnotations;

namespace BlogManagement.WebAPI.ViewModel
{
	public class UserModel
	{
		public string Fullname { get; set; }
		[Required]
		public string Email { get; set; }
		public string PhoneNumber { get; set; }
	}
}
