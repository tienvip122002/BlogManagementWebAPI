using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Authorization;

namespace BlogManagement.WebAPI.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController: ControllerBase
	{
		ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll()
		{
			return Ok(await _categoryService.GetCategoryAll());
		}
	}
}
