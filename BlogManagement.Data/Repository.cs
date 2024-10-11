using BlogManagement.Data.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data
{
	internal class Repository<T> : IRepository<T> where T : class
	{
		BlogManagementWebAPIContext _blogManagementWebAPIContext;
		public Repository(BlogManagementWebAPIContext BlogManagementWebAPIContext)
		{
			_blogManagementWebAPIContext = BlogManagementWebAPIContext;
		}
		public IEnumerable<T> GetAll()
		{
			return _blogManagementWebAPIContext.Set<T>();
		}
		public IEnumerable<T> GetByCondition(Expression<Func<T, bool>> expression)
		{
			return _blogManagementWebAPIContext.Set<T>().Where(expression);
		}
		public void Insert(T entity)
		{
			_blogManagementWebAPIContext.Set<T>().AddAsync(entity);
		}

		public void Insert(IEnumerable<T> entities)
		{
			_blogManagementWebAPIContext.Set<T>().AddRangeAsync(entities);
		}

		public void Update(T entity)
		{
			EntityEntry entityEntry = _blogManagementWebAPIContext.Entry<T>(entity);
			entityEntry.State = EntityState.Modified;
		}
		public void Delete(T entity)
		{
			EntityEntry entityEntry = _blogManagementWebAPIContext.Entry<T>(entity);
			entityEntry.State = EntityState.Deleted;
		}

		public void Delete(Expression<Func<T, bool>> expression)
		{
			var entities = _blogManagementWebAPIContext.Set<T>().Where(expression).ToList();

			if (entities.Count > 0)
			{
				_blogManagementWebAPIContext.Set<T>().RemoveRange(entities);
			}
		}
		public void Commit()
		{
			_blogManagementWebAPIContext.SaveChanges();
		}
	}
}
