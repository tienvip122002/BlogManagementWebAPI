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
			service.AddScoped(typeof(IRepository<>), typeof(IRepository<>));
		}
	}
}
