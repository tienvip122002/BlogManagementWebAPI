using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using BlogManagement.Data;

namespace BlogManagement.WebAPI.Controllers
{
	[ApiExplorerSettings(GroupName = CommonConstants.Routes.GroupAdmin)]
	[Route(CommonConstants.Routes.BaseRouteAdmin)]
	public class AboutUsController : BaseController<AboutUsController, AboutUs, AboutUsCreateVModel, AboutUsUpdateVModel, AboutUsGetByIdVModel, AboutUsGetAllVModel>
	{
		private readonly IAboutUsService _AboutUsService;

		public AboutUsController(IAboutUsService AboutUsService)
			: base(AboutUsService)
		{
			_AboutUsService = AboutUsService;
		}

		[HttpGet]
		public async Task<IActionResult> Search(FiltersGetAllVModel model, string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				// Call GetAllPagination without keyword
				var response = await _AboutUsService.GetAll(model);
				var result = new ObjectResult(response);
				if (!response.Success)
				{
					//log
				}
				return result;
			}
			else
			{
				// Call Search with keyword
				var response = await _AboutUsService.Search(model, keyword);
				var result = new ObjectResult(response);
				if (!response.Success)
				{
					//log
				}
				return result;
			}
		}
	}
}
