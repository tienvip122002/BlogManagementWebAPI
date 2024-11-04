using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public abstract class GenericEntity
	{
		public long Id { get; set; }
		public bool? IsActive { get; set; }
	}
}
