using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using static BlogManagement.Domain.Constants.MsgConstants;

namespace BlogManagement.Domain.VModels
{
	public class ArticleCreateVModel
	{
		[Required(ErrorMessage = "Name is required.")]
		[MinLength(6, ErrorMessage = "Name must be at least 6 characters long.")]
		public string Name { get; set; } = null!;
		[JsonIgnore]
		public string Alias { get; set; }
		[Required(ErrorMessage = "ShortDescription is required.")]
		[MinLength(6, ErrorMessage = "ShortDescription must be at least 6 characters long.")]
		public string ShortDescription { get; set; }
		[Required(ErrorMessage = "Description is required.")]
		[MinLength(6, ErrorMessage = "Description must be at least 6 characters long.")]
		public string Description { get; set; }
		[Required]
		public long? ThumbnailFileId { get; set; }
		public bool IsHighlight { get; set; }
		[JsonIgnore]
		public long CountView { get; set; }
		public string UserId { get; set; } = null!;
		[Required]
		public long? CategoryId { get; set; }
		public long? AttachmentFileId { get; set; }
		[Required]
		public bool IsActive { get; set; }
	}
	public class ArticleUpdateVModel : ArticleCreateVModel
	{
		public long Id { get; set; }
		public string UpdatedBy { get; set; }
	}
	public class ArticleGetAllVModel : ArticleUpdateVModel
	{
		public DateTime? CreatedDate { get; set; }
		public DateTime? UpdatedDate { get; set; }
		public string AuthorName { get; set; }
		public string CategoryName { get; set; }
		public string ThumbnailURL { get; set; }
	}
	public class FilterGetAllExtraVModel : FiltersGetAllVModel
	{
		public long? CategoryId { get; set; }
	}

	public class ArticleGetByIdVModel : ArticleGetAllVModel
	{
		public SysFileGetByIdVModel AttachmentFileInfo { get; set; }
		public SysFileGetByIdVModel ThumbnailFileInfo { get; set; }
	}

	[DataContract]
	public class ArticleExport
	{
		[DataMember(Name = @"Id")]
		public long Id { get; set; }
		[DataMember(Name = @"Tên")]
		public string Name { get; set; }
		[DataMember(Name = @"ShortDescription")]
		public string ShortDescription { get; set; }
		[DataMember(Name = @"Description")]
		public string Description { get; set; }
		[DataMember(Name = @"CreatedDate")]
		public DateTime? CreatedDate { get; set; }
	}
	public class ArticleWithUserDetails
	{
		public ArticleGetAllVModel Article { get; set; }
	}
}
