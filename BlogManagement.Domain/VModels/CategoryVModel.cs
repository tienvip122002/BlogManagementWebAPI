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
	public class CategoryCreateVModel
	{
		[Required]
		[MinLength(3, ErrorMessage = "Too short Category Name!")]
		public string Name { get; set; }
		[Required]
		[RegularExpression(@"^[0-9]+$", ErrorMessage = "Please submit a valid Sort Number!")]
		public int? Sort { get; set; } = null;
		public long? ThumbnailFileId { get; set; }
		[JsonIgnore]
		public string? ThumbnailFileURL { get; set; }
		public long? ParentId { get; set; }
		public bool? IsActive { get; set; }
	}
	public class CategoryUpdateVModel : CategoryCreateVModel
	{
		[Required]
		public long Id { get; set; }
	}
	public class CategoryGetAllAsTreeVModel
	{
		public long Id { get; set; }
		public string Name { get; set; }
		public string Alias { get; set; }
		public int? Sort { get; set; }
		public long? ThumbnailFileId { get; set; }
		public string? ThumbnailFileURL { get; set; }
		public long? ParentId { get; set; }
		public ICollection<CategoryGetAllAsTreeVModel>? Children { get; set; }
		public bool? IsActive { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
	}
	public class CategoryGetAllVModel : CategoryUpdateVModel
	{
		public string Alias { get; set; }
		public DateTime? CreatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string UpdatedBy { get; set; }
		public SysFileGetByIdVModel ThumnailFileInfo { get; set; }
	}
	public class CategoryGetByIdVModel : CategoryGetAllVModel
	{
	}

	[DataContract]
	public class CategoryExport
	{
		[DataMember(Name = @"Id")]
		public long Id { get; set; }
		[DataMember(Name = @"Tên")]
		public string Name { get; set; }
		[DataMember(Name = @"Alias")]
		public string Alias { get; set; }
		[DataMember(Name = @"Sort")]
		public int? Sort { get; set; }
		[DataMember(Name = @"ThumbnailFileId")]
		public long? ThumbnailFileId { get; set; }
		[DataMember(Name = @"ThumbnailFileURL")]
		public string? ThumbnailFileURL { get; set; }
		[DataMember(Name = @"ParentId")]
		public long? ParentId { get; set; }
		[DataMember(Name = @"IsActive")]
		public bool IsActive { get; set; }
		[DataMember(Name = @"CreatedDate")]
		public DateTime? CreatedDate { get; set; }
		[DataMember(Name = @"UpdateDate")]
		public DateTime? UpdatedDate { get; set; }
	}
}

