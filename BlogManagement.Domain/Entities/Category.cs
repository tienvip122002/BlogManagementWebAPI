using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public class Category: BaseEntitie
	{
		public string Name { get; set; } = null!;
		public string? Alias { get; set; }
		public string? Description { get; set; }
	}
}
