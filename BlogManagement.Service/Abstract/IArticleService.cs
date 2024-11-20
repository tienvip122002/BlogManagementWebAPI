using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
	public interface IArticleService : IBaseService<Article, ArticleCreateVModel, ArticleUpdateVModel, ArticleGetByIdVModel, ArticleGetAllVModel>
	{
		Task<ResponseResult> GetAllArticlesByCategoryId(long id);
		Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword);
		Task<ResponseResult> GetAllExtra(FilterGetAllExtraVModel model);
	}
}