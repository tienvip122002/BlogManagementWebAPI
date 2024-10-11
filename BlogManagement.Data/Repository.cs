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
	public class Repository<T> : IRepository<T> where T : class
	{
		BlogManagementWebAPIContext _blogManagementWebAPIContext;
		public Repository(BlogManagementWebAPIContext BlogManagementWebAPIContext)
		{
			_blogManagementWebAPIContext = BlogManagementWebAPIContext;
		}
		public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null)
		{
			if (expression == null)
			{
				return await _blogManagementWebAPIContext.Set<T>().ToListAsync();
			}

			return await _blogManagementWebAPIContext.Set<T>().Where(expression).ToListAsync();
		}

		public async Task<T> GetByIdAsync(object id)
		{
			return await _blogManagementWebAPIContext.Set<T>().FindAsync(id);
		}

		public async Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression = null)
		{
			return await _blogManagementWebAPIContext.Set<T>().FirstOrDefaultAsync();
		}

		public async Task InsertAsync(T entity)
		{
			await _blogManagementWebAPIContext.Set<T>().AddAsync(entity);
		}

		public async Task InsertAsync(IEnumerable<T> entities)
		{
			await _blogManagementWebAPIContext.Set<T>().AddRangeAsync(entities);
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

		public IQueryable<T> Table => _blogManagementWebAPIContext.Set<T>();

		public async Task CommitAsync()
		{
			await _blogManagementWebAPIContext.SaveChangesAsync();
		}
	}
}
