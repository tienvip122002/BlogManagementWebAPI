using AutoMapper;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.VModels;

namespace BlogManagement.WebAPI.Configuration
{
	public class SysFileMapping : Profile
	{
		public SysFileMapping()
		{
			//Insert
			CreateMap<SysFileCreateVModel, SysFile>();
			// Update
			CreateMap<SysFileUpdateVModel, SysFile>();
			//Get All 
			CreateMap<SysFile, SysFileGetAllVModel>();
			//Get By Id
			CreateMap<SysFile, SysFileGetByIdVModel>();
			//Get list
			CreateMap<SysFile, SysFileExportVModel>();
		}
	}
}
