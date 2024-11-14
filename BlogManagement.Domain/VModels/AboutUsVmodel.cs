using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Domain.VModels
{
	public class AboutUsCreateVModel
	{
		[Required(ErrorMessage = "Title is required.")]
		[MinLength(6, ErrorMessage = "Title must be at least 6 characters long.")]
		public string Title { get; set; } = null!;

		public string Description { get; set; }

		public long? SysFileId { get; set; }

		[Required(ErrorMessage = "IsActive status is required")]
		public bool IsActive { get; set; }
	}

	public class AboutUsUpdateVModel : AboutUsCreateVModel
	{
		public long Id { get; set; }
	}

	public class AboutUsGetAllVModel : AboutUsUpdateVModel
	{
	}
	public class AboutUsGetByIdVModel : AboutUsUpdateVModel
	{
	}
	[DataContract]
	public class AboutUsExport
	{
		[DataMember(Name = @"Title")]
		public string Title { get; set; }
		[DataMember(Name = @"Description")]
		public string Description { get; set; }

		[DataMember(Name = @"SysFileld")]
		public long? SysFileId { get; set; }

		[DataMember(Name = @"CreatedDate")]
		public DateTime? CreatedDate { get; set; }

		[DataMember(Name = @"CreatedBy")]
		public string CreatedBy { get; set; }

		[DataMember(Name = @"UpdatedDate")]
		public DateTime? UpdatedDate { get; set; }

		[DataMember(Name = @"UpdatedBy")]
		public string UpdatedBy { get; set; }
	}
}
