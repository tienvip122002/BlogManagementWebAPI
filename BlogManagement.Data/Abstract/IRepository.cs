using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BlogManagement.Data.Abstract
{
	public interface IRepository <T> where T : class
	{
		/// <summary>
		/// Get data by expression
		/// </summary>
		/// <param name="expression"></param>
		/// <returns></returns>
		void Insert(T entity);
		void Insert(IEnumerable<T> entities);
		void Update(T entity);
		void Delete(T entity);
		void Delete(Expression<Func<T, bool>> expression);
	}
}
