using BlogManagement.Domain.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.VModels
{
	public class FiltersGetAllVModel
	{
		public bool? IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
		public int PageSize { get; set; } = CommonConstants.ConfigNumber.pageSizeDefault;
		public int PageNumber { get; set; } = 1;
		public string SortBy { get; set; }
		public bool IsExport { get; set; } = false;
		public bool IsDescending { get; set; } = true;
	}

	public class FiltersGetAllByQueryStringVModel
	{
		public string Keyword { get; set; }
		public string CategoryId { get; set; }
		public string CreatedDate { get; set; }
		public bool? IsActive { get; set; }
		public string OrderBy { get; set; }
		public string OrderDirection { get; set; }
		public int PageSize { get; set; } = CommonConstants.ConfigNumber.pageSizeDefault;
		public int PageNumber { get; set; } = 1;
	}
}
