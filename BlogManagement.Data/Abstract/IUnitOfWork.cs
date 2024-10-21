using BlogManagement.Domain.Entities;
using System.Threading.Tasks;

namespace BlogManagement.Data.Abstract
{
    public interface IUnitOfWork
    {
        Repository<User> RepositoryUser { get; }
        Repository<UserToken> RepositoryUserToken { get; }
		Repository<Category> RepositoryCategory { get; }

		Task CommitAsync();
	}
}