using System.Threading.Tasks;

namespace BlogManagement.Infrastructure.CommonService
{
	public interface IEmailTemplateReader
	{
		Task<string> GetTemplate(string templateName);
	}
}