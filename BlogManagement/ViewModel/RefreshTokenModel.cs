using System.ComponentModel.DataAnnotations;

namespace BlogManagement.WebAPI.ViewModel
{
	public class RefreshTokenModel
	{
		[Required]
		public string RefreshToken { get; set; }
	}
}
