using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BlogManagement.Data
{
	public class BlogManagementWebAPIContext: IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		private readonly IConfiguration _configuration;
		private readonly IServiceProvider _serviceProvider;
		public BlogManagementWebAPIContext(DbContextOptions<BlogManagementWebAPIContext> options, IConfiguration configuration, IServiceProvider serviceProvider)
			: base(options)
		{
			_configuration = configuration;
			_serviceProvider = serviceProvider;
		}
		//public DbSet<ApplicationUser> ApplicationUser { get; set; } = null!;
		public DbSet<Category> Category { get; set; }
		public DbSet<Article> Article { get; set; }
		public DbSet<UserToken> UserToken { get; set; }
		public DbSet<DBLog> DBLog { get; set; }
		public DbSet<AboutUs> AboutUs { get; set; }
		public DbSet<SysFile> SysFile { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			modelBuilder.Entity<IdentityRole>().ToTable("Role");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");

			SeedData(modelBuilder);
		}

		private void SeedData(ModelBuilder modelBuilder)
		{
			string username = _configuration["DefaultUser:Username"];
			string email = _configuration["DefaultUser:Email"];
			string defaultRole = _configuration["DefaultUser:Role"];
			string password = _configuration["DefaultUser:Password"];

			using var scope = _serviceProvider.CreateScope();
			var passwordHasherService = scope.ServiceProvider.GetService<PasswordHasher<ApplicationUser>>();

			var roles = _configuration.GetSection("DefaultRole");

			if (roles.Exists())
			{
				foreach (var role in roles.GetChildren())
				{
					string roleId = Guid.NewGuid().ToString();

					modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
					{
						Id = roleId,
						Name = role.Value,
						NormalizedName = role.Value.ToUpper(),
					});

					if (role.Value == defaultRole)
					{
						defaultRole = roleId;
					}
				}
			}

			string userId = Guid.NewGuid().ToString();

			modelBuilder.Entity<ApplicationUser>().HasData(
				new ApplicationUser
				{
					Id = userId,
					UserName = username.ToLower(),
					NormalizedUserName = username.ToUpper(),
					Email = email,
					NormalizedEmail = email.ToUpper(),
					AccessFailedCount = 0,
					PasswordHash = passwordHasherService.HashPassword(new ApplicationUser
					{
						UserName = username.ToLower(),
						NormalizedUserName = username.ToUpper(),
						Email = email,
						NormalizedEmail = email.ToUpper()
					}, password)
				});

			modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>
			{
				RoleId = defaultRole,
				UserId = userId,
			});
		}
	}
}
