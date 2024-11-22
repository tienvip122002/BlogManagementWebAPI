using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
//using NLog;
using BlogManagement.core.Abstract;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using Microsoft.Extensions.Logging;
using BlogManagement.Domain.Entities;

namespace BlogManagement.WebAPI.Controllers
{
	[ApiExplorerSettings(GroupName = CommonConstants.Routes.GroupAdmin)]
	[Route(CommonConstants.Routes.BaseRouteAdmin)]
	public class CategoryController : BaseController<CategoryController, Category, CategoryCreateVModel, CategoryUpdateVModel, CategoryGetByIdVModel, CategoryGetAllVModel>
	{
		private readonly ICategoryService _categoryService;
		private readonly ILogger _logger;
		public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) : base(categoryService, logger)
		{
			_categoryService = categoryService;
			_logger = logger;
		}

		[HttpGet]
		public virtual async Task<IActionResult> GetAllAsTree(FiltersGetAllVModel model)
		{
			var result = await _categoryService.GetAllAsTree(model);
			return new ObjectResult(result);
		}

		[HttpGet]
		public virtual async Task<IActionResult> Search(FiltersGetAllVModel model, string keyword)
		{
			var response = new ResponseResult();
			if (!string.IsNullOrEmpty(keyword))
			{
				response = await _categoryService.Search(model, keyword);
			}
			else
			{
				response = await _categoryService.GetAll(model);
			}
			var result = new ObjectResult(response);
			if (!response.Success)
			{
				_logger.LogWarning(CommonConstants.LoggingEvents.GetItem, MsgConstants.ErrorMessages.ErrorGetById, _nameController);
			}
			return result;
		}
	}
}
