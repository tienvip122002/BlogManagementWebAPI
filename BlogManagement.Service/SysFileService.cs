using AutoMapper;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Configurations;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace BlogManagement.Service
{
	public class SysFileService
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IGenericRepository<SysFile> _sysfileRepo;
		private readonly JwtIssuerOptions _jwtIssuerOptions;
		private readonly IMapper _mapper;
		private readonly string _tempFolder;
		private readonly string _tempPath;
		private readonly int _chunkSize;
		private readonly string[] _medias = { CommonConstants.FileType.Audio, CommonConstants.FileType.Image, CommonConstants.FileType.Video, CommonConstants.FileType.Document };
		private readonly IWebHostEnvironment _env;
		private readonly UploadConfigurations _uploadConfigs;
	}
}
