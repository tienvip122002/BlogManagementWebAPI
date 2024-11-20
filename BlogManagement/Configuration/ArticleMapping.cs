using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.VModels;

namespace BlogManagement.WebAPI.Configuration
{
	public class ArticleMapping : Profile
	{
		private readonly string _urlServer;
		public ArticleMapping(string urlServer)
		{
			_urlServer = urlServer;
			//Insert
			CreateMap<ArticleCreateVModel, Article>();

			// Update
			CreateMap<ArticleUpdateVModel, Article>()
				.ForMember(dest => dest.CountView, opt => opt.Ignore())
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember, destMember) => srcMember != null));
			//Get All 
			CreateMap<Article, ArticleGetAllVModel>()
				.ForMember(dest => dest.ThumbnailURL, opt => opt.MapFrom(src => GetThumbnailUrl(src)))
				.ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => $"{src.User.Fullname}"));
			CreateMap<Article, ArticleWithUserDetails>();
			//.ForMember(desc => desc.CreatedDate, src => src.MapFrom(x => x.CreatedDate.Value.ToString("yyyy-MM-dd h:mm")));
			//Get By Id
			CreateMap<Article, ArticleGetByIdVModel>();
			//Get list
			//CreateMap<List<Config>, List<ConfigVModel>>();
			CreateMap<Article, ArticleExport>();
		}
		string GetThumbnailUrl(Article source)
		{
			return source?.ThumbnailFile != null ? $"{_urlServer}/{source.ThumbnailFile.Path}" : null;
		}
	}
}
