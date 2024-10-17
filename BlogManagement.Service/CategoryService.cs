using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Service.Abstract;
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
		public async Task<List<Category>> GetCategoryAll()
		{
			return await _categoryRepository.GetAllAsync();
		}
	}
}
