using AutoMapper;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class AboutUsService : BaseService<AboutUs, AboutUsCreateVModel, AboutUsUpdateVModel, AboutUsGetByIdVModel, AboutUsGetAllVModel, AboutUsExport>, IAboutUsService
	{
		private readonly IRepository<AboutUs> _aboutUsRepo;
		private readonly IMapper _mapper;
		private readonly IHttpContextAccessor _httpContextAccessor;
		public AboutUsService(IRepository<AboutUs> aboutUsRepo, IMapper mapper, IHttpContextAccessor httpContextAccessor) : base(aboutUsRepo, mapper)
		{
			_aboutUsRepo = aboutUsRepo;
			_mapper = mapper;
			_httpContextAccessor = httpContextAccessor;
		}

		public Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword)
		{
			throw new NotImplementedException();
		}
	}
}
