using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using NLog;

namespace BlogManagement.WebAPI.Controllers
{
	//[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController: ControllerBase
	{
		ICategoryService _categoryService;
		private readonly ILogger _loggerDb = LogManager.GetLogger("logDatabaseTarget");
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetCategoryAll()
		{
			_loggerDb.Error("log err db");
			_loggerDb.Info("log err db");
			return Ok(await _categoryService.GetCategoryAll());
		}
	}
}
