﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Model
{
	public class EmailRequest
	{
		public string To { get; set; }
		public string Subject { get; set; }
		public string Content { get; set; }
		public string[] AttachmentFilePaths { get; set; } = Array.Empty<string>();
	}
}
