using Microsoft.Extensions.DependencyInjection;
using BlogManagement.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using BlogManagement.Data.Abstract;
using Microsoft.IdentityModel.Tokens;
using BlogManagement.Service;
using BlogManagement.Service.Abstract;
using BlogManagement.Authentication.Service;
using BlogManagement.core.Abstract;
using BlogManagement.core.Cache;
using Microsoft.AspNetCore.Identity;
using BlogManagement.Domain.Entities;
using BlogManagement.core.Configuration;
using BlogManagement.core.EmailHelper;
using BlogManagement.Infrastructure.CommonService;

namespace BlogManagement.Infrastructure.Configuration
{
    public static class ConfigurationService
	{
		public static void RegisterContextDb(this IServiceCollection service, IConfiguration configuration)
		{
			service.AddDbContext<BlogManagementWebAPIContext>(options => options
							.UseSqlServer(configuration.GetConnectionString("BlogManagement"),
							 options => options.MigrationsAssembly(typeof(BlogManagementWebAPIContext).Assembly.FullName)));

			service.AddIdentity<ApplicationUser, IdentityRole>()
				.AddEntityFrameworkStores<BlogManagementWebAPIContext>()
				.AddDefaultTokenProviders();

			service.Configure<IdentityOptions>(config =>
			{
				config.Password.RequireNonAlphanumeric = false;
				config.Password.RequireDigit = true;
				config.Password.RequiredLength = 2;
				config.Password.RequireLowercase = true;
				config.Password.RequireUppercase = true;
			});

			service.Configure<DataProtectionTokenProviderOptions>(options =>
			{
				options.TokenLifespan = TimeSpan.FromHours(10);
			});
		}
		public static void RegisterDI(this IServiceCollection service, IConfiguration configuration)
		{
			service.Configure<EmailConfig>(configuration.GetSection("MailSettings"));


			service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			service.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
			service.AddScoped(typeof(IDapperHelper<>), typeof(DapperHelper<>));

			service.AddSingleton<IDistributedCacheService, DistributedCacheService>();

			service.AddScoped<IEmailHelper, EmailHelper>(); 
			service.AddScoped<IEmailTemplateReader, EmailTemplateReader>();
			service.AddScoped<ICategoryService, CategoryService>();
			service.AddScoped<IUserService, UserService>();
			service.AddScoped<ITokenHandler, Authentication.Service.TokenHandler>();
			service.AddScoped<IUserTokenService, UserTokenService>();
			service.AddScoped<PasswordHasher<ApplicationUser>>();
			service.AddScoped<PasswordValidator<ApplicationUser>>();
		}
	}
}
