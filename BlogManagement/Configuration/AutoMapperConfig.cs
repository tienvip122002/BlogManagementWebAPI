using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.WebAPI.ViewModel;

namespace BlogManagement.WebAPI.Configuration
{
	public class AutoMapperConfig : Profile
	{
        public AutoMapperConfig()
        {
			CreateMap<UserModel, User>()
					  .ForMember(dest => dest.DisplayName, y => y.MapFrom(src => src.Fullname))
					  .ReverseMap();
		}
    }
}
