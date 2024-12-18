﻿using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogManagement.WebAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class ErrorController : ControllerBase
	{
		[HttpGet]
		public IActionResult Index()
		{

			var msg = HttpContext.Features.Get<IExceptionHandlerFeature>();

			int statusCode = HttpContext.Response.StatusCode;

			return Ok();
		}
	}
}
