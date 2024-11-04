using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogManagement.Data.Abstract
{
	public interface IRepository<T> where T : BaseEntitie
	{
		IQueryable<T> Table { get; }
		/// <summary>
		/// Get data by expression
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression = null);
		Task<T> GetByIdAsync(object id);
		Task<T> GetSingleByConditionAsync(Expression<Func<T, bool>> expression = null);
		Task InsertAsync(T entity);
		Task InsertAsync(IEnumerable<T> entities);
		Task<Pagination> GetAllPagination(int pageNumber, int pageSize, Expression<Func<T, bool>> where = null,
		   Expression<Func<T, dynamic>> orderDesc = null, Expression<Func<T, dynamic>> orderAsc = null);
		Task<T> GetById(long id);
		Task<ResponseResult> Create(T entity);
		Task<ResponseResult> Update(T entity);
		Task<ResponseResult> UpdateMany(IEnumerable<T> entity);
		Task<ResponseResult> Remove(long id);
		Task<ResponseResult> RemoveAll(IEnumerable<T> entities);
		Task<bool> SaveChanges(ResponseResult result);
		IQueryable<T> AsQueryable();
		Task<IEnumerable<T>> Where(Expression<Func<T, bool>> where);
		void EntryReference(T entity, Expression<Func<T, dynamic>> entityReference);
		void EntryCollection(T entity, Expression<Func<T, IEnumerable<dynamic>>> entityCollection);
	}
}
