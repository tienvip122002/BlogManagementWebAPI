using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.VModels
{
	public class ExportFileVModel
	{
		[Required]
		public string FileName { get; set; }

		[Required]
		public string SheetName { get; set; }

		[Required]
		public string Type { get; set; }
	}

}
