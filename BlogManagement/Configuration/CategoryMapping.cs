using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.VModels;
using System.Linq;

namespace BlogManagement.WebAPI.Configuration
{
	public class CategoryMapping : Profile
	{
		private readonly string _urlServer;
		public CategoryMapping(string urlServer)
		{
			//Insert
			CreateMap<CategoryCreateVModel, Category>();
			//Update
			CreateMap<CategoryUpdateVModel, Category>();
			//GetAll
			CreateMap<Category, CategoryGetAllVModel>();
			CreateMap<Category, CategoryGetAllAsTreeVModel>()
				.ForMember(cate => cate.Children, next => next.MapFrom(obj => obj.Children.Select(child => new CategoryGetAllAsTreeVModel
				{
					Id = child.Id,
					Name = child.Name,
					Alias = child.Alias,
					Sort = child.Sort,
					ThumbnailFileId = child.ThumbnailFileId,
					ParentId = child.ParentId,
					IsActive = child.IsActive,
					CreatedDate = child.CreatedDate,
					CreatedBy = child.CreatedBy,
					UpdatedDate = child.UpdatedDate,
					UpdatedBy = child.UpdatedBy
				})));
			//GetById, GetByParentId
			CreateMap<Category, CategoryGetByIdVModel>();
			CreateMap<Category, CategoryExport>().ForMember(dest => dest.ThumbnailFileURL, opt => opt.MapFrom(src => src != null ? GetThumbnailUrl(src) : null));

		}

		string GetThumbnailUrl(Category source)
		{
			return source?.ThumbnailFile != null ? $"{_urlServer}/{source.ThumbnailFile.Path}" : null;
		}
	}
}
