using BlogManagement.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data
{
	public class GlobalVariables
	{
		private static IHttpContextAccessor _contextAccessor;
		public GlobalVariables(IHttpContextAccessor contextAccessor)
		{
			_contextAccessor = contextAccessor;
		}
		private static ClaimsPrincipal User => _contextAccessor.HttpContext.User;
		protected static string GlobalUserName => User.Identity.IsAuthenticated ? User.Identity.Name : string.Empty;
		protected static string GlobalUserId => User.Identity.IsAuthenticated ? User.FindFirst(CommonConstants.SpecialFields.id)?.Value : string.Empty;
	}
}
