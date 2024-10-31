using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.CommonService
{
	public class EmailTemplateReader : IEmailTemplateReader
	{
		private readonly IWebHostEnvironment _webHostEnvironment;

		public EmailTemplateReader(IWebHostEnvironment webHostEnvironment)
		{
			_webHostEnvironment = webHostEnvironment;
		}

		public async Task<string> GetTemplate(string templateName)
		{
			string templateEmail = Path.Combine(_webHostEnvironment.ContentRootPath, templateName);

			string content = await File.ReadAllTextAsync(templateEmail);

			return content;
		}
	}
}
