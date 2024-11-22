using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
	public interface ICategoryService : IBaseService<Category, CategoryCreateVModel, CategoryUpdateVModel, CategoryGetByIdVModel, CategoryGetAllVModel>
	{
		Task<ResponseResult> GetAllAsTree(FiltersGetAllVModel model);
		Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword);
		//Task<ResponseResult> Update(CmsCategoryCreateVModel model);
	}
}