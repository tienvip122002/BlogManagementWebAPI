using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Data
{
    public class GenericRepository<T> : IGenericRepository<T> where T : GenericEntity
	{
		private readonly BlogManagementWebAPIContext _dbContext;
		private DbSet<T> _entities;
		//private readonly ILogger _logger;
		public GenericRepository(BlogManagementWebAPIContext dbContext)
		{
			_dbContext = dbContext ?? throw new ArgumentNullException("context");
			_entities = dbContext.Set<T>();
		}
		public async Task<Pagination> GetAllPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> where = null,
			Expression<Func<T, dynamic>> orderDesc = null, Expression<Func<T, dynamic>> orderAsc = null)
		{
			IQueryable<T> query = _entities;
			if (where != null)
			{
				query = query.Where(where);
			}
			if (orderAsc != null)
			{
				query = query.OrderBy(orderAsc);
			}
			if (orderDesc != null)
			{
				query = query.OrderByDescending(orderDesc);
			}
			var data = await Task.FromResult(query.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList());
			return new Pagination
			{
				Records = data,
				TotalRecords = query.Count()
			};
		}

		public async Task<T> GetById(long id)
		{
			var entity = await _entities.FindAsync(id);
			return entity;
		}

		public async Task<ResponseResult> Create(T entity)
		{
			var result = new ResponseResult();
			if (entity != null)
			{
				entity.IsActive = entity.IsActive.HasValue ? entity.IsActive.Value : CommonConstants.Status.InActive;
				_entities.Add(entity);
				result.Data = await SaveChanges(result) ? entity : null;
			}
			return result;
		}

		public async Task<ResponseResult> Update(T entity)
		{
			var result = new ResponseResult();
			if (entity != null)
			{
				T data = await GetById(entity.Id);
				if (data != null)
				{
					_dbContext.Entry(data).CurrentValues.SetValues(entity);
					result.Data = await SaveChanges(result) ? entity : null;
				}
			}
			return result;
		}

		public async Task<ResponseResult> UpdateMany(IEnumerable<T> entities)
		{
			var result = new ResponseResult();
			if (entities.Any())
			{
				foreach (var entity in entities)
				{
					T data = await GetById(entity.Id);
					if (data != null)
					{
						_dbContext.Entry(data).CurrentValues.SetValues(entity);
						result.Data = await SaveChanges(result) ? entity : null;
					}

				}
			}
			return result;
		}

		public async Task<ResponseResult> Remove(long id)
		{
			var result = new ResponseResult();
			T entity = await GetById(id);
			if (entity != null)
			{
				_entities.Remove(entity);
				await SaveChanges(result);
			}
			return result;
		}

		public async Task<ResponseResult> RemoveAll(IEnumerable<T> entities)
		{
			var result = new ResponseResult();
			_entities.RemoveRange(entities);
			await SaveChanges(result);
			return result;
		}

		public IQueryable<T> AsQueryable()
		{
			var query = _entities.AsQueryable();
			return query;
		}

		public async Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where)
		{
			return await Task.FromResult(_entities.Where(where).ToList());
		}

		public void EntryReference(T entity, Expression<Func<T, dynamic>> entityReference)
		{
			_dbContext.Entry(entity).Reference(entityReference).Load();
		}

		public void EntryCollection(T entity, Expression<Func<T, IEnumerable<dynamic>>> entityCollection)
		{
			_dbContext.Entry(entity).Collection(entityCollection).Load();
		}

		public async Task<bool> SaveChanges(ResponseResult result)
		{
			try
			{
				result.Success = await _dbContext.SaveChangesAsync() >= 0;
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result.Success;
		}

	}
}
