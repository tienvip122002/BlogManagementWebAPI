using BlogManagement.Domain.Entities;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
    public interface IUserService
    {
        Task<User> CheckLogin(string username, string password);
		Task<User> FindById(long userId);
		Task<User> FindByUsername(string username);
	}
}