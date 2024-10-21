using BlogManagement.Data;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class UserService : IUserService
	{
		private readonly IUnitOfWork _unitOfWork;
		public UserService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}

		public async Task<User> CheckLogin(string username, string password)
		{
			return await _unitOfWork.RepositoryUser.GetSingleByConditionAsync(x => x.UserName == username && x.Password == password);
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
