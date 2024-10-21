using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Entities
{
	public class DBLog
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Required]
		public int Id { get; set; }
		[StringLength(50)]
		public string LoggedDate { get; set; }
		public string Level { get; set; }
		public string Message { get; set; }
		public string Logger { get; set; }
	}
}
