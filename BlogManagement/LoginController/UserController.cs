﻿using AutoMapper;
using BlogManagement.core.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Infrastructure.CommonService;
using BlogManagement.WebAPI.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.LoginController
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;
        private readonly PasswordHasher<ApplicationUser> _passwordHasher;
        private readonly PasswordValidator<ApplicationUser> _passwordValidator;
        private readonly IEmailHelper _emailHelper;
        private readonly IEmailTemplateReader _emailTemplateReader;
        public UserController(
            IMapper mapper,
            UserManager<ApplicationUser> userManager,
            PasswordHasher<ApplicationUser> passwordHasher,
            PasswordValidator<ApplicationUser> passwordValidator,
            IEmailHelper emailHelper,
            IEmailTemplateReader emailTemplateReader,
            IConfiguration configuration)
        {
            _mapper = mapper;
            _userManager = userManager;
            _passwordHasher = passwordHasher;
            _passwordValidator = passwordValidator;
            _emailHelper = emailHelper;
            _emailTemplateReader = emailTemplateReader;
            _configuration = configuration;
        }

        [HttpPost]
        public async Task<IActionResult> Register(CancellationToken cancellationToken, [FromBody] UserModel userVM)
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
                await _userManager.AddToRoleAsync(user, "User");

                //Send Email for Confirm
                string token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                string url = Url.Action("ConfirmEmail", "User", new { memberKey = user.Id, tokenReset = token }, Request.Scheme);

                string body = await _emailTemplateReader.GetTemplate("Templates\\ConfirmEmail.html");
                body = string.Format(body, user.Fullname, url);

                await _emailHelper.SendEmailAsync(cancellationToken, new EmailRequest
                {
                    To = user.Email,
                    Subject = "Confirm Email For Register",
                    Content = body
                });

                return Ok(true);
            }
            else
                return BadRequest(result.Errors);
        }

        [HttpGet("confirm-email")]
        public async Task<IActionResult> ConfirmEmail(string memberKey, string tokenReset)
        {
            var user = await _userManager.FindByIdAsync(memberKey);

            if (user is null)
            {
                return BadRequest("Account is exist in the system");
            }

            if (user.EmailConfirmed)
            {
                return Ok("The email has already been confirmed");
            }

            var identityResult = await _userManager.ConfirmEmailAsync(user, tokenReset);

            if (identityResult.Succeeded)
            {
                return Ok("Your account has been actived");
            }

            return BadRequest("Confirm email failed.");
        }

        [HttpGet("forget-password")]
        public async Task<IActionResult> ForgetPassword(CancellationToken cancellationToken, string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user is null)
            {
                return BadRequest("Email is not exist");
            }

            string host = _configuration.GetValue<string>("ApplicationUrl");

            string tokenConfirm = await _userManager.GeneratePasswordResetTokenAsync(user);

            string encodedToken = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(tokenConfirm));

            string resetPasswordUrl = $"{host}reset-password?email={email}&token={encodedToken}";

            string body = $"Please reset your password by clicking here: <a href=\"{resetPasswordUrl}\">link</a>";

            await _emailHelper.SendEmailAsync(cancellationToken, new EmailRequest
            {
                To = user.Email,
                Subject = "Reset Password",
                Content = body
            });

            return Ok("Please check your email");
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel resetPasswordMd)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordMd.Email);

            if (user is null)
            {
                return BadRequest("Email is not exist");
            }

            if (string.IsNullOrEmpty(resetPasswordMd.Token))
            {
                return BadRequest("Token is invalid");
            }

            string decodedToken = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordMd.Token));

            var identityResult = await _userManager.ResetPasswordAsync(user, decodedToken, resetPasswordMd.Password);

            if (!identityResult.Succeeded)
            {
                return BadRequest(identityResult.Errors.ToList()[0].Description);
            }

            return Ok("Reset password successful");

        }
    }
}
