using BlogManagement.Domain.Entities;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
    public interface IUserTokenService
    {
		Task<UserToken> CheckRefreshToken(string code);
    }
}