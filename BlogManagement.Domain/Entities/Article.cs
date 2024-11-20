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
		public long? ThumbnailFileId { get; set; }
		public bool? IsHighlight { get; set; }
		public long CountView { get; set; }
		public string? UserId { get; set; } = null!;
		public long? CategoryId { get; set; }
		public long? AttachmentFileId { get; set; }

		public virtual SysFile? AttachmentFile { get; set; }
		public virtual Category? Category { get; set; }
		public virtual SysFile? ThumbnailFile { get; set; }
		[ForeignKey("UserId")]
		public virtual ApplicationUser User { get; set; }
	}
}
