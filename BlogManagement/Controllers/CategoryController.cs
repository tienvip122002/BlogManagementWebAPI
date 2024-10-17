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
		public IActionResult Index()
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
