using BlogManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
    public interface ICategoryService
    {
        Task<List<Category>> GetCategoryAll();
    }
}