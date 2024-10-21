using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.Entities;

namespace BlogManagement.Data
{
	public class BlogManagementWebAPIContext: DbContext
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
		}
	}
}
