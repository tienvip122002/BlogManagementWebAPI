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

namespace BlogManagement.Infrastructure.Configuration
{
    public static class ConfigurationService
	{
		public static void RegisterContextDb(this IServiceCollection service, IConfiguration configuration)
		{
			service.AddDbContext<BlogManagementWebAPIContext>(options => options
							.UseSqlServer(configuration.GetConnectionString("BlogManagement"),
							 options => options.MigrationsAssembly(typeof(BlogManagementWebAPIContext).Assembly.FullName)));
		}
		public static void RegisterDI(this IServiceCollection service)
		{
			service.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			service.AddScoped<IDapperHelper, DapperHelper>();

			service.AddScoped<ICategoryService, CategoryService>();
			service.AddScoped<IUserService, UserService>();
			service.AddScoped<ITokenHandler, Authentication.Service.TokenHandler>();
			service.AddScoped<IUserTokenService, UserTokenService>();
		}
	}
}
