using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.WebAPI.ViewModel;

namespace BlogManagement.WebAPI.Configuration
{
	public class AutoMapperConfig : Profile
	{
        public AutoMapperConfig()
        {
			CreateMap<UserModel, ApplicationUser>()
					.ForMember(dest => dest.PasswordHash, y => y.MapFrom(src => src.Password))
					.ReverseMap();
		}
    }
}
