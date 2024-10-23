using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Threading.Tasks;

namespace BlogManagement.WebAPI.Middleware
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _requestDelegate;

		private readonly ILogger _logger = LogManager.GetLogger("logCategory1Target");

		public ExceptionMiddleware(RequestDelegate requestDelegate)
		{
			_requestDelegate = requestDelegate;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			try
			{
				await _requestDelegate.Invoke(context);
			}
			catch (ArgumentOutOfRangeException ex)
			{
				_logger.Error(ex);
				await context.Response.WriteAsync($" {context.Response.StatusCode} - Error is outside of index");
			}
			catch (ArgumentNullException ex)
			{
				_logger.Error(ex);
				await context.Response.WriteAsync($" {context.Response.StatusCode} - Object is null");
			}
			catch (System.Exception ex)
			{
				_logger.Error(ex);
				await context.Response.WriteAsync($" {context.Response.StatusCode} - {ex.Message}");
			}
		}
	}
}
