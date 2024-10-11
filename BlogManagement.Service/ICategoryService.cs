using BlogManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
	public interface ICategoryService
	{
		Task<IEnumerable<Category>> GetCategories();
	}
}