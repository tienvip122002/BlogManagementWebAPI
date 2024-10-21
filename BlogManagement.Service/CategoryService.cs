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
		IUnitOfWork _unitOfWork;
		public CategoryService(IUnitOfWork unitOfWork)
		{
			_unitOfWork = unitOfWork;
		}
		public async Task<List<Category>> GetCategoryAll()
		{
			return await _unitOfWork.RepositoryCategory.GetAllAsync();
		}
	}
}
