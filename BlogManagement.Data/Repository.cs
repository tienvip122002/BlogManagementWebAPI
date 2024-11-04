using BlogManagement.Data.Abstract;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Constants;

namespace BlogManagement.Data
{
	public class Repository<T> : IRepository<T> where T : BaseEntitie
	{
		BlogManagementWebAPIContext _blogManagementWebAPIContext;
		private DbSet<T> _entities;
		public Repository(BlogManagementWebAPIContext BlogManagementWebAPIContext)
		{
			_blogManagementWebAPIContext = BlogManagementWebAPIContext;
			_entities = BlogManagementWebAPIContext.Set<T>();
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

		public async Task<Pagination> GetAllPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> where = null,
		   Expression<Func<T, dynamic>> orderDesc = null, Expression<Func<T, dynamic>> orderAsc = null)
		{
			IQueryable<T> query = _entities; //.AsNoTracking(); if use AsNoTracking, you cant use EntryReference()
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
				entity.CreatedDate = DateTime.Now;
				//entity.CreatedBy = GlobalUserName;
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
					entity.UpdatedDate = DateTime.Now;
					//entity.UpdatedBy = GlobalUserName;
					_blogManagementWebAPIContext.Entry(data).CurrentValues.SetValues(entity);
					_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedDate).IsModified = false;
					_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedBy).IsModified = false;
					result.Data = await SaveChanges(result) ? entity : null;
				}
			}
			return result;
		}
		public async Task<ResponseResult> UpdateCountView(T entity)
		{
			var result = new ResponseResult();
			if (entity != null)
			{
				T data = await GetById(entity.Id);
				if (data != null)
				{
					_blogManagementWebAPIContext.Entry(data).CurrentValues.SetValues(entity);
					_blogManagementWebAPIContext.Entry(data).Property(e => e.IsActive).IsModified = false;
					_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedBy).IsModified = false;
					_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedDate).IsModified = false;
					_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedBy).IsModified = false;
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
						entity.UpdatedDate = DateTime.Now;
						//entity.UpdatedBy = GlobalUserName;
						_blogManagementWebAPIContext.Entry(data).CurrentValues.SetValues(entity);
						_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedDate).IsModified = false;
						_blogManagementWebAPIContext.Entry(data).Property(e => e.CreatedBy).IsModified = false;
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
			_blogManagementWebAPIContext.Entry(entity).Reference(entityReference).Load();
		}
		public void EntryCollection(T entity, Expression<Func<T, IEnumerable<dynamic>>> entityCollection)
		{
			_blogManagementWebAPIContext.Entry(entity).Collection(entityCollection).Load();
		}

		public IQueryable<T> Table => _blogManagementWebAPIContext.Set<T>();

		public async Task<bool> SaveChanges(ResponseResult result)
		{
			try
			{
				result.Success = await _blogManagementWebAPIContext.SaveChangesAsync() >= 0;
			}
			catch (Exception ex)
			{
				//logging
				result.Success = false;
			}
			return result.Success;
		}
	}
}
