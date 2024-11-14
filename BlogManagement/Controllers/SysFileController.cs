using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Controllers
{
	[ApiExplorerSettings(GroupName = CommonConstants.Routes.GroupAdmin)]
	[Route(CommonConstants.Routes.BaseRouteAdmin)]
	public class SysFileController : ControllerBase
	{
		private readonly ISysFileService _sysfileService;

		public SysFileController(ISysFileService sysfileService)
		{
			_sysfileService = sysfileService;
		}

		[HttpPost]
		public async Task<IActionResult> Create([FromBody] SysFileCreateVModel model)
		{
			if (model == null)
			{
				return new BadRequestObjectResult(MsgConstants.ErrorMessages.ErrorCreate);
			}

			var result = await _sysfileService.Create(model);
			var objectResult = new ObjectResult(result);

			if (result.Success)
			{
				return objectResult;
			}
			else
			{
				return new BadRequestObjectResult(result);
			}
		}
		[HttpGet]
		public async Task<IActionResult> GetAllByType(string fileType, int pageSize = CommonConstants.ConfigNumber.pageSizeDefault, int pageNumber = 1)
		{
			ObjectResult objecrResult;
			if (string.IsNullOrWhiteSpace(fileType))
			{
				objecrResult = new BadRequestObjectResult(CommonConstants.Validate.inputInvalid);
			}
			else
			{
				ResponseResult response = await _sysfileService.GetAllByType(fileType, pageSize, pageNumber);
				if (response.Success)
				{
					objecrResult = new ObjectResult(response)
					{
						StatusCode = (int)HttpStatusCode.OK
					};
				}
				else
				{
					objecrResult = new ObjectResult(response)
					{
						StatusCode = (int)HttpStatusCode.InternalServerError
					};
				}
			}
			return objecrResult;
		}
		[HttpPut]
		public async Task<IActionResult> Update([FromBody] SysFileUpdateVModel model)
		{
			if (model == null)
			{
				return new BadRequestObjectResult(MsgConstants.ErrorMessages.ErrorUpdate);
			}

			var result = await _sysfileService.Update(model);
			var objectResult = new ObjectResult(result);

			if (result.Success)
			{
				return objectResult;
			}
			else
			{
				return new BadRequestObjectResult(result);
			}
		}
		[HttpPost]
		public async Task<IActionResult> UploadImageBase64([FromBody] SysFileCreateBase64VModel model)
		{
			ObjectResult result;
			if (!ModelState.IsValid)
			{
				result = new BadRequestObjectResult(ModelState);
			}
			else
			{
				var responseResult = await _sysfileService.CreateBase64(model);
				result = new ObjectResult(responseResult);
				if (!responseResult.Success)
				{
					
				}
			}
			return result;
		}
		[HttpPost]
		public async Task<IActionResult> FileChunks([FromForm] FileChunk fileChunk)
		{
			ObjectResult result;

			if (string.IsNullOrWhiteSpace(fileChunk.FileName))
			{
				result = new BadRequestObjectResult(CommonConstants.Validate.inputInvalid);
			}
			else
			{
				var response = await _sysfileService.FileChunks(fileChunk);
				result = new ObjectResult(response);
				if (!response.Success)
				{
					
				}
			}
			return result;
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] SysFileGetAllVModel model)
		{
			var result = await _sysfileService.GetAll(model);

			if (result.Success)
			{
				return Ok(new { Success = result.Success, Message = result.Message, Data = result.Data });
			}

			return BadRequest(new { Message = result.Message });
		}
		[HttpPost]
		public async Task<IActionResult> ChangeStatus(long id)
		{
			var result = await _sysfileService.ChangeStatus(id);
			var objectResult = new ObjectResult(result);

			if (result.Success)
			{
				return objectResult;
			}
			else
			{
				return new BadRequestObjectResult(result);
			}
		}
		[HttpDelete]
		public async Task<IActionResult> Remove(long id)
		{
			if (id <= 0)
			{
				return new BadRequestObjectResult(string.Format(MsgConstants.Error404Messages.FieldIsInvalid, "Id"));
			}
			var response = await _sysfileService.Remove(id);
			var result = new ObjectResult(response);

			if (response.Success)
			{
				return result;
			}
			else
			{
				return new BadRequestObjectResult(response);
			}
		}

		[HttpPost]
		public async Task<IActionResult> UploadAvatar([FromForm] FileChunk fileChunk)
		{
			if (string.IsNullOrWhiteSpace(fileChunk.FileName))
			{
				return new BadRequestObjectResult(CommonConstants.Validate.inputInvalid);
			}

			var response = await _sysfileService.UploadAvatar(fileChunk);
			var result = new ObjectResult(response);

			if (response.Success)
			{
				return result;
			}
			else
			{
				return new BadRequestObjectResult(response);
			}
		}
		[HttpDelete(CommonConstants.Routes.Url)]
		public async Task<IActionResult> RemoveByUrl(string url)
		{
			if (string.IsNullOrEmpty(url))
			{
				return new BadRequestObjectResult(CommonConstants.Validate.inputInvalid);
			}
			var response = await _sysfileService.RemoveByUrl(url);
			return new ObjectResult(response);
		}
	}
}
