using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.VModels;

namespace BlogManagement.WebAPI.Configuration
{
	public class AboutUsMapping : Profile
	{
		public AboutUsMapping()
		{
			CreateMap<AboutUsUpdateVModel, AboutUs>()
				.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));
			CreateMap<AboutUs, AboutUsGetAllVModel>();
			CreateMap<AboutUs, AboutUsGetByIdVModel>();
			// Export
			CreateMap<AboutUs, AboutUsExport>();
			CreateMap<AboutUs, AboutUsCreateVModel>().ReverseMap();

		}
	}
}
