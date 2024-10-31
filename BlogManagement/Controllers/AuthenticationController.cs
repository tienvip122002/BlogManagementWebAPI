using BlogManagement.Authentication.Service;
using BlogManagement.Service.Abstract;
using BlogManagement.WebAPI.ViewModel;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthenticationController: ControllerBase
	{
		IUserService _userService;
		ITokenHandler _tokenHandler;
		IUserTokenService _userTokenService;
		public AuthenticationController(IUserService userService, ITokenHandler tokenHandler, IUserTokenService userTokenService)
		{
			_userService = userService;
			_tokenHandler = tokenHandler;
			_userTokenService = userTokenService;
		}

		[HttpPost("login")]
		[AllowAnonymous]
		public async Task<IActionResult> Login([FromServices] IValidator<AccountModel> validator, [FromBody] AccountModel accountModel)
		{
			//var validations = await validator.ValidateAsync(accountModel);
			//if (!validations.IsValid)
			//{
			//	return BadRequest(validations.Errors.Select(x => new ErrorValdations
			//	{
			//		FieldName = x.PropertyName,
			//		ErrorMessage = x.ErrorMessage
			//	}));
			//}

			var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password);

			if (user == null)
			{
				return Unauthorized();
			}

			(string accessToken, DateTime expiresAtas) = await _tokenHandler.CreateAccessToken(user);
			(string code, string refreshToken, DateTime expiresAtre) = await _tokenHandler.CreateRefreshToken(user);


			return Ok(new JwtModel
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				FullName = user.Fullname,
				UserName = user.UserName,
				AccessTokenExpireDate = expiresAtas.ToString("yyyy/MM/dd hh:mm:ss"),
			});
		}

		[HttpPost("refresh-token")]
		public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel token)
		{
			if (token == null)
				return BadRequest("Could not get refresh token");

			return Ok(await _tokenHandler.ValidateRefreshToken(token.RefreshToken));
		}
	}
}
