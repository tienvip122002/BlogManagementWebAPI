using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Model
{
	public class Pagination
	{
		public long TotalRecords { get; set; }
		public IEnumerable<dynamic> Records { get; set; }
	}
}
