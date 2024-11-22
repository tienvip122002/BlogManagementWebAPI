using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Controllers
{
	[ApiExplorerSettings(GroupName = CommonConstants.Routes.GroupAdmin)]
	[Route(CommonConstants.Routes.BaseRouteAdmin)]
	public class ArticleController : BaseController<ArticleController, Article, ArticleCreateVModel, ArticleUpdateVModel, ArticleGetByIdVModel, ArticleGetAllVModel>
	{
		private readonly IArticleService _articleService;
		private readonly ILogger _logger;
		private readonly string GlobalUserId;
		private readonly IHttpContextAccessor _contextAccessor;

		public ArticleController(IArticleService cmsArticleService, IHttpContextAccessor contextAccessor, ILogger<ArticleController> logger)
			: base(cmsArticleService, logger)
		{
            _logger = logger;
			_articleService = cmsArticleService;
			_contextAccessor = contextAccessor;
			GlobalUserId = _contextAccessor.HttpContext.User.Identity.IsAuthenticated ? _contextAccessor.HttpContext.User.FindFirst(CommonConstants.SpecialFields.id)?.Value : string.Empty;
		}
		//Combine GetALl - SoretedArticles
		[HttpPost]
		public override async Task<IActionResult> Create([FromBody] ArticleCreateVModel model)
		{
			ObjectResult result;
			if (!ModelState.IsValid)
			{
				result = new BadRequestObjectResult(ModelState);
			}
			else
			{
				// Kiểm tra xem UserId đã được cung cấp trong model hay chưa
				if (string.IsNullOrEmpty(model.UserId))
				{
					// Nếu không, sử dụng GlobalUserId
					model.UserId = GlobalUserId;
				}

				model.Alias = Utilities.SlugGenerator.GenerateAliasFromName(model.Name);
				var response = await _articleService.Create(model);
				result = new ObjectResult(response);
				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.CreateItem, MsgConstants.ErrorMessages.ErrorCreate);
				}
			}
			return result;
		}
		[HttpGet]
		public async Task<IActionResult> GetAllExtra([FromQuery] FilterGetAllExtraVModel model)
		{
			ObjectResult result;

			try
			{
				var response = await _articleService.GetAllExtra(model);
				result = new ObjectResult(response);

				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.GetItem, response.Message);
				}
			}
			catch (Exception ex)
			{
				//_logger.LogError(CommonConstants.LoggingEvents.GetItem, ex, MsgConstants.ErrorMessages.ErrorGetById);
				result = new ObjectResult(new ResponseResult
				{
					Success = false,
					Message = MsgConstants.ErrorMessages.ErrorGetById
				});
			}

			return result;
		}

		[HttpPut]
		public override async Task<IActionResult> Update([FromBody] ArticleUpdateVModel model)
		{
			ObjectResult result;
			if (!ModelState.IsValid || ((dynamic)model).Id <= 0)
			{
				result = new BadRequestObjectResult(ModelState);
			}
			else
			{
				model.Alias = Utilities.SlugGenerator.GenerateAliasFromName(model.Name);
				var response = await _articleService.Update(model);
				result = new ObjectResult(response);
				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.UpdateItem, MsgConstants.ErrorMessages.ErrorUpdate, _nameController);
				}
			}
			return result;
		}

		[HttpGet]
		public async Task<IActionResult> GetAllArticlesByCategoryId(long id)
		{
			ObjectResult result;
			try
			{
				var response = await _articleService.GetAllArticlesByCategoryId(id);
				result = new ObjectResult(response);

				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.GetItem, MsgConstants.ErrorMessages.ErrorGetById);
				}
			}
			catch (Exception ex)
			{
				//_logger.LogError(CommonConstants.LoggingEvents.GetItem, ex, MsgConstants.ErrorMessages.ErrorGetById);
				result = new ObjectResult(new ResponseResult
				{
					Success = false,
					Message = MsgConstants.ErrorMessages.ErrorGetById
				});
			}

			return result;
		}
		[HttpGet]
		public virtual async Task<IActionResult> Search(FiltersGetAllVModel model, string keyword)
		{
			if (string.IsNullOrWhiteSpace(keyword))
			{
				// Call GetAllPagination without keyword
				var response = await _articleService.GetAll(model);
				var result = new ObjectResult(response);
				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.GetItem, MsgConstants.ErrorMessages.ErrorGetById, _nameController);
				}
				return result;
			}
			else
			{
				// Call Search with keyword
				var response = await _articleService.Search(model, keyword);
				var result = new ObjectResult(response);
				if (!response.Success)
				{
					//_logger.LogWarning(CommonConstants.LoggingEvents.GetItem, MsgConstants.ErrorMessages.ErrorGetById, _nameController);
				}
				return result;
			}
		}
	}
}
