using BlogManagement.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BlogManagement.Domain.VModels
{
	public class SysFileCreateVModel
	{
		[JsonIgnore]
		public long Id { get; set; }
		public string Name { get; set; } = null!;
		[JsonIgnore]
		public string Path { get; set; } = null!;
		[JsonIgnore]
		public string? Type { get; set; }
	}
	public class SysFileCreateBase64VModel : SysFileCreateVModel
	{
		[Required]
		public string Base64String { get; set; }
		[JsonIgnore]
		public string Path { get; set; }
		[JsonIgnore]
		public string? Type { get; set; }
	}
	public class FileChunk
	{
		public string FileName { get; set; }
		public IFormFile File { get; set; }
	}
	public class SysFileBase64ToFileVModel
	{
		public Guid SessionId { get; set; }
		public int PartNumber { get; set; }
		public string FileName { get; set; }
		public string Base64 { get; set; }
		public bool IsEnd { get; set; }
	}
	public class SysFileUpdateVModel : SysFileCreateVModel
	{
		public long Id { get; set; }
	}
	public class SysFileGetAllVModel : SysFileUpdateVModel
	{
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string CreatedBy { get; set; }
		public string UpdatedBy { get; set; }
		public int PageNumber { get; set; } = 1;
		public int PageSize { get; set; } = CommonConstants.ConfigNumber.pageSizeDefault;
		public bool? IsActive { get; set; }
		public string SortBy { get; set; }
	}
	public class SysFileGetByIdVModel : SysFileUpdateVModel
	{
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }

	}

	[DataContract]
	public class SysFileExportVModel
	{
		[DataMember(Name = @"Id")]
		public long Id { get; set; }
		[DataMember(Name = @"Name")]
		public string Name { get; set; } = null!;
		[DataMember(Name = @"Path")]
		public string Path { get; set; } = null!;
		[DataMember(Name = @"Type")]
		public string? Type { get; set; }
		[DataMember(Name = @"CreatedDate")]
		public DateTime? CreatedDate { get; set; }
		[DataMember(Name = @"CreatedBy")]
		public string CreatedBy { get; set; }
		[DataMember(Name = @"UpdatedDate")]
		public DateTime? UpdatedDate { get; set; }
		[DataMember(Name = @"UpdatedBy")]
		public string UpdatedBy { get; set; }
		[DataMember(Name = @"IsActive")]
		public bool? IsActive { get; set; }
	}
}
