using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Threading;
using BlogManagement.Service;

namespace BlogManagement.WebAPI.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class CategoryController: Controller
	{
		ICategoryService _categoryService;
		public CategoryController(ICategoryService categoryService)
		{
			_categoryService = categoryService;
		}

		[Route("")]
		public async Task<IActionResult> Index()
		{
			return Ok(await _categoryService.GetCategories());
		}
	}
}
