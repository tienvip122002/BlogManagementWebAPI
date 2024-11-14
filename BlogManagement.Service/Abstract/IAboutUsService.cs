using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
	public interface IAboutUsService : IBaseService<AboutUs, AboutUsCreateVModel, AboutUsUpdateVModel, AboutUsGetByIdVModel, AboutUsGetAllVModel>
	{
		Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword);
	}
}