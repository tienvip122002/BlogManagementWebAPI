﻿using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Service.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class UserTokenService : IUserTokenService
	{
		IRepository<UserToken> _userTokenRepository;

		public UserTokenService(IRepository<UserToken> userTokenRepository)
		{
			_userTokenRepository = userTokenRepository;
		}

		//public async Task SaveToken(UserToken userToken)
		//{
		//	await _userTokenRepository.InsertAsync(userToken);
		//	await _userTokenRepository.SaveChanges();
		//}
		public async Task<UserToken> CheckRefreshToken(string code)
		{
			return await _userTokenRepository.GetSingleByConditionAsync(x => x.CodeRefreshToken == code);
		}
	}
}
