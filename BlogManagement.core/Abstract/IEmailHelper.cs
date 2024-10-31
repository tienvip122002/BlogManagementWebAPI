using BlogManagement.Domain.Model;
using System.Threading;
using System.Threading.Tasks;

namespace BlogManagement.core.Abstract
{
    public interface IEmailHelper
    {
        Task SendEmailAsync(CancellationToken cancellationToken, EmailRequest emailRequest);
    }
}