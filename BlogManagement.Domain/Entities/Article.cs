using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public class Article : BaseEntitie
	{
		public string Name { get; set; } = null!;
		public string? Alias { get; set; }
		public string? ShortDescription { get; set; }
		public string? Description { get; set; }
		public long CountView { get; set; }
		public long? CategoryId { get; set; }
		[ForeignKey("CategoryId")]
		public Category Category { get; set; }
	}
}
