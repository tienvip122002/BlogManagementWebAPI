using System.Threading.Tasks;

namespace BlogManagement.core.Abstract
{
    public interface IDistributedCacheService
    {
        Task<T> Get<T>(string key);
        Task Remove(string key);
        Task Set<T>(string key, T value);
    }
}