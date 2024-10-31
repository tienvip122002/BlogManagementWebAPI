using BlogManagement.Data;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class UserService : IUserService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly IUnitOfWork _unitOfWork;
		public UserService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager)
		{
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public async Task<ApplicationUser> CheckLogin(string username, string password)
		{
			var user = await _userManager.FindByNameAsync(username);

			if (user == null)
			{
				return default(ApplicationUser);
			}

			//string plainPassword = AESEncryption.DecryptStringAES(encryptedPassword, key);

			var hasExist = await _userManager.CheckPasswordAsync(user, password);

			if (!hasExist)
			{
				return default(ApplicationUser);
			}

			return user;
		}

		public async Task<User> FindByUsername(string username)
		{
			return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.UserName == username);
		}

		public async Task<User> FindById(long userId)
		{
			return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.Id == userId);
		}
	}
}
