using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace BlogManagement.Data
{
	public class BlogManagementWebAPIContext: IdentityDbContext<ApplicationUser, IdentityRole, string>
	{
		public BlogManagementWebAPIContext(DbContextOptions<BlogManagementWebAPIContext> options)
			: base(options)
		{

		}
		public DbSet<Category> Category { get; set; }
		public DbSet<Article> Article { get; set; }
		public DbSet<User> User { get; set; }
		public DbSet<UserToken> UserToken { get; set; }
		public DbSet<DBLog> DBLog { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<ApplicationUser>().ToTable("ApplicationUser");
			modelBuilder.Entity<IdentityRole>().ToTable("Role");
			modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole");
		}
	}
}
