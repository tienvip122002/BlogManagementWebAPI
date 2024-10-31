using Microsoft.Win32.SafeHandles;

namespace BlogManagement.WebAPI.ViewModel
{
	public class JwtModel
	{
		public string AccessToken {  get; set; }
		public string RefreshToken {  get; set; }
		public string UserName {  get; set; }
		public string FullName {  get; set; }
		public string AccessTokenExpireDate { get; set; }
	}
}
