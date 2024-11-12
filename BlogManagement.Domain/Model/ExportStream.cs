using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Model
{
	public class ExportStream
	{
		public MemoryStream Stream { get; set; }
		public string FileName { get; set; }
		public string ContentType { get; set; }
	}
}
