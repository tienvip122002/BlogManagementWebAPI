using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public class Category: BaseEntitie
	{
		public Category()
		{
			Article = new HashSet<Article>();
			Children = new HashSet<Category>();
		}

		public string Name { get; set; } = null!;
		public string? Alias { get; set; }
		public int? Sort { get; set; }
		public long? ThumbnailFileId { get; set; }
		public long? ParentId { get; set; }

		public virtual Category? Parent { get; set; }
		public virtual SysFile? ThumbnailFile { get; set; }
		public virtual ICollection<Article>? Article { get; set; }
		public virtual ICollection<Category>? Children { get; set; }
	}
}
