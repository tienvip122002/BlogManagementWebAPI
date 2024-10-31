using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Authorization;
using NLog;
using BlogManagement.core.Abstract;

namespace BlogManagement.WebAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController: ControllerBase
	{
		ICategoryService _categoryService;
		IEmailHelper _emailHelper;
		//private readonly ILogger _loggerDb = LogManager.GetLogger("logDatabaseTarget");
		public CategoryController(ICategoryService categoryService, IEmailHelper emailHelper)
		{
			_categoryService = categoryService;
			_emailHelper = emailHelper;
		}


		/// <summary>
		/// Get all categories
		/// </summary>
		/// <returns></returns>
		/// <exception cref="System.ArgumentNullException"></exception>
		[HttpGet]
		[Produces("text/json")]
		public async Task<IActionResult> GetAllCategory(CancellationToken cancellationToken)
		{
			//throw new System.ArgumentNullException();
			var result = await _categoryService.GetCategoryAll();

			await _emailHelper.SendEmailAsync(cancellationToken, new Domain.Model.EmailRequest
			{
				To = "tientiengviet@gmail.com",
				Subject = "This is test for sending mail from NetCore",
				Content = "No Content!!!!!!!!!!!!!!!!!!!!!!!!!!!!!"
			});

			return Ok(result);
		}
	}
}
