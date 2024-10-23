﻿using BlogManagement.Authentication.Service;
using BlogManagement.Service.Abstract;
using BlogManagement.WebAPI.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
		public async Task<IActionResult> Login([FromBody] AccountModel accountModel)
		{
			if (accountModel == null)
			{
				return BadRequest("account is not exit");
			}

			var user = await _userService.CheckLogin(accountModel.Username, accountModel.Password);

			if (user == null)
			{
				return Unauthorized();
			}

			(string accessToken, DateTime expiresAtas) = await _tokenHandler.CreateAccessToken(user);
			(string code, string refreshToken, DateTime expiresAtre) = await _tokenHandler.CreateRefreshToken(user);

			await _userTokenService.SaveToken(new Domain.Entities.UserToken
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				CodeRefreshToken = code,
				ExpiredDateAccessToken = expiresAtas,
				ExpiredDateRefreshToken = expiresAtre,
				CreatedDate = DateTime.Now,
				UserId = user.Id,
				IsActive = true
			});

			return Ok(new JwtModel
			{
				AccessToken = accessToken,
				RefreshToken = refreshToken,
				FullName = user.DisplayName,
				UserName = user.UserName
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