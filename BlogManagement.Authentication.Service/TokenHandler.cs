using BlogManagement.Domain.Entities;
using BlogManagement.Service.Abstract;
using BlogManagement.WebAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Authentication.Service
{
	public class TokenHandler : ITokenHandler
	{
		IConfiguration _configuration;
		IUserService _userService;
		IUserTokenService _userTokenService;
		UserManager<ApplicationUser> _userManager;

		private const string TokenBearIssuer = "TokenBear:Issuer";
		private const string TokenBearAudience = "TokenBear:Audience";
		private const string TokenBearSignatureKey = "TokenBear:SignatureKey";
		private const string TokenBearExpiredTimeByMinutes = "TokenBear:AccessTokenExpiredByMinutes";
		private const string AccessTokenProvider = "AccessTokenProvider";
		private const string RefreshTokenProvider = "RefreshTokenProvider";
		private const string AccessToken = "AccessToken";
		private const string RefreshToken = "RefreshToke";

		public TokenHandler(IConfiguration configuration, IUserService userService, IUserTokenService userTokenService, UserManager<ApplicationUser> userManager)
		{
			_configuration = configuration;
			_userService = userService;
			_userTokenService = userTokenService;
			_userManager = userManager;
		}
		public async Task<(string, DateTime)> CreateAccessToken(ApplicationUser user)
		{
			int tokenExpirationMinutes = int.Parse(_configuration["TokenBear:AccessTokenExpiredByMinutes"]);
			DateTime expiresAt = DateTime.Now.AddMinutes(tokenExpirationMinutes);

			var roles = await _userManager.GetRolesAsync(user);

			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Aud, "BlogManagementWebAPI", ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(ClaimTypes.Name, user.Fullname ?? "Unknown", ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim("Username", user.UserName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
			}
			.Union(roles.Select(x => new Claim(ClaimTypes.Role, x)));

			//claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenInfo = new JwtSecurityToken(
				issuer: _configuration["TokenBear:Issuer"],
				audience: _configuration["TokenBear:Audience"],
				claims: claims,
				notBefore: DateTime.Now,
				expires: expiresAt,
				credentials
			);

			string accessToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

			await _userManager.SetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken", accessToken);

			return await Task.FromResult((accessToken, expiresAt));
		}

		public async Task<(string, string, DateTime)> CreateRefreshToken(ApplicationUser user)
		{
			int refreshTokenExpirationHours = int.Parse(_configuration["TokenBear:RefreshTokenExpiredByHours"]);
			DateTime expiresAt = DateTime.Now.AddHours(refreshTokenExpirationHours);
			string refreshTokenCode = Guid.NewGuid().ToString();
			var claims = new Claim[]
			{
				new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString(), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Iss, _configuration["TokenBear:Issuer"], ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToString(), ClaimValueTypes.DateTime, _configuration["TokenBear:Issuer"]),
				new Claim(JwtRegisteredClaimNames.Exp, expiresAt.ToString("yyyy/MM/dd hh:mm:ss"), ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim(ClaimTypes.SerialNumber, refreshTokenCode, ClaimValueTypes.String, _configuration["TokenBear:Issuer"]),
				new Claim("Username", user.UserName, ClaimValueTypes.String, _configuration["TokenBear:Issuer"])
			};

			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"]));

			var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var tokenInfo = new JwtSecurityToken(
				issuer: _configuration["TokenBear:Issuer"],
				audience: _configuration["TokenBear:Audience"],
				claims: claims,
				notBefore: DateTime.Now,
				expires: expiresAt,
				credentials
			);

			string refreshToken = new JwtSecurityTokenHandler().WriteToken(tokenInfo);

			await _userManager.SetAuthenticationTokenAsync(user, "RefreshTokenProvider", "RefreshToken", refreshToken);


			return await Task.FromResult((refreshTokenCode, refreshToken, expiresAt));
		}

		public async Task ValidateToken(TokenValidatedContext context)
		{
			var claims = context.Principal.Claims.ToList();

			if (claims.Count == 0)
			{
				context.Fail("This token contains no information");
				return;
			}

			var identity = context.Principal.Identity as ClaimsIdentity;

			if (identity.FindFirst(JwtRegisteredClaimNames.Iss) == null)
			{
				context.Fail("This token is not issued by point entry");
				return;
			}

			if (identity.FindFirst("Username") != null)
			{
				string username = identity.FindFirst("Username").Value;

				var user = await _userService.FindByUsername(username);

				if (user == null)
				{
					context.Fail("This token is invalid for user");
					return;
				}
			}
		}

		public async Task<JwtModel> ValidateRefreshToken(string refreshToken)
		{
			JwtModel jwtModel = new();

			var cliamPriciple = new JwtSecurityTokenHandler().ValidateToken(
					refreshToken,
					new TokenValidationParameters
					{
						RequireExpirationTime = true,
						ValidateIssuer = false,
						ValidateAudience = false,
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["TokenBear:SignatureKey"])),
						ValidateIssuerSigningKey = true,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					},
					out _
				);
			if(cliamPriciple == null) return new();

			string username = cliamPriciple.Claims.FirstOrDefault(x => x.Type == "Username")?.Value;

			var user = await _userManager.FindByNameAsync(username);

			var token = await _userManager.GetAuthenticationTokenAsync(user, "AccessTokenProvider", "AccessToken");

			if (!string.IsNullOrEmpty(token))
			{
				(string newAccessToken, DateTime createdDate) = await CreateAccessToken(user);
				(string codeRefreshToken, string newRefreshToken, DateTime newCreatedDate) = await CreateRefreshToken(user);
				return new JwtModel
				{
					AccessToken = newAccessToken,
					RefreshToken = newRefreshToken,
					FullName = user.Fullname,
					UserName = user.UserName
				};
			}

			return new();
		}
	}
}
