using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.WebAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly IMapper _mapper;
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly PasswordHasher<ApplicationUser> _passwordHasher;
		private readonly PasswordValidator<ApplicationUser> _passwordValidator;
		public UserController(
			IMapper mapper, 
			UserManager<ApplicationUser> userManager, 
			PasswordHasher<ApplicationUser> passwordHasher, 
			PasswordValidator<ApplicationUser> passwordValidator) 
		{ 
			_mapper = mapper;
			_userManager = userManager;
			_passwordHasher = passwordHasher;
			_passwordValidator = passwordValidator;
		}

		[HttpPost]
		public async Task<IActionResult> Register([FromBody] UserModel userVM)
		{
			if (userVM == null)
			{
				return BadRequest("Invalid Data");
			}
			var user = _mapper.Map<ApplicationUser>(userVM);

			var validationPassword = await _passwordValidator.ValidateAsync(_userManager, user, userVM.Password);

			if (!validationPassword.Succeeded)
			{
				return BadRequest(validationPassword.Errors);
			}

			user.PasswordHash = _passwordHasher.HashPassword(user, user.PasswordHash);

			var result = await _userManager.CreateAsync(user);

			if (result.Succeeded)
			{
				return Ok(true);
			}
			else
				return BadRequest(result.Errors);
		}	
	}
}
