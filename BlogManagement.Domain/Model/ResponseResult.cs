using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Model
{
	public class ResponseResult
	{
		public bool Success { get; set; }
		public string Message { get; set; }
		public dynamic Data { get; set; }
		//public int StatusCode {  get; set; }
	}
}
