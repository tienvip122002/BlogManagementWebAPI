using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public partial class AboutUs : BaseEntitie
	{
		public long SysFileId { get; set; } // Khóa ngoại liên kết đến SysFile

		[ForeignKey("SysFileId")]
		public SysFile SysFile { get; set; } // Navigation property tới SysFile
		public string Title { get; set; }
		public string Description { get; set; }
	}
}
