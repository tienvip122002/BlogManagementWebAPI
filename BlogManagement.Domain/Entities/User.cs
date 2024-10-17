using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public class User: BaseEntitie
	{
		[Required]
		[StringLength(150)]
		public string UserName {  get; set; }
		[Required]
		[StringLength(150)]
		public string Password { get; set; }
		public string DisplayName { get; set; }
		public DateTime LastLoggerIn { get; set; }

	}
}
