using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
	public class CategoryService : ICategoryService
	{
		IRepository<Category> _categoryRepository;
		IDapperHelper _categoryDapperHelper;
		public CategoryService(IRepository<Category> categoryRepository, IDapperHelper categoryDapperHelper)
		{
			_categoryRepository = categoryRepository;
			_categoryDapperHelper = categoryDapperHelper;
		}
		public async Task<IEnumerable<Category>> GetCategories()
		{
			string sql = $"SELECT * FROM Category";
			return await _categoryDapperHelper.ExecuteSqlReturnListAsync<Category>(sql);
		}
	}
}
