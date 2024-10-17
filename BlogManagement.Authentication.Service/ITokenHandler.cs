using BlogManagement.Domain.Entities;
using BlogManagement.WebAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System;
using System.Threading.Tasks;

namespace BlogManagement.Authentication.Service
{
	public interface ITokenHandler
	{
		Task<(string, DateTime)> CreateAccessToken(User user);
		Task<(string, string, DateTime)> CreateRefreshToken(User user);
		Task<JwtModel> ValidateRefreshToken(string refreshToken);
		Task ValidateToken(TokenValidatedContext context);
	}
}