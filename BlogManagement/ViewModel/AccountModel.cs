﻿using BlogManagement.Service.Abstract;
using FluentValidation;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Threading;

namespace BlogManagement.WebAPI.ViewModel
{
	public class AccountModel
	{
		public string Username { get; set; }
		public string Password { get; set; }
	}
	public class LoginValidator : AbstractValidator<AccountModel>
	{
		IUserService _userService;

		public LoginValidator(IUserService userService)
		{
			_userService = userService;

			RuleFor(m => m.Username)
				.NotEmpty()
				.EmailAddress()
				.WithMessage("{PropertyName} is invalid")
				.MustAsync(Exist)
				.WithMessage("{PropertyName} is exist, please choose another");

			RuleFor(m => m.Password)
				.NotEmpty()
				//.MinimumLength(3)
				//.MinimumLength(20)
				.WithMessage("Password is greater 3 and less than 20")
				.Must((password) =>
				{
					if (password.Length < 3) { return false; }

					return true;
				});
		}
		private async Task<bool> Exist(string username, CancellationToken cancellationToken)
		{
			var user = _userService.FindByUsername(username);

			if (user == null) { return true; }

			return false;
		}
	}
}
