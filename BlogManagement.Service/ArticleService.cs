using AutoMapper;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Configurations;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class ArticleService : BaseService<Article, ArticleCreateVModel, ArticleUpdateVModel, ArticleGetByIdVModel, ArticleGetAllVModel, ArticleExport>, IArticleService
	{
		private readonly IRepository<Article> _articleRepo;
		private readonly IRepository<Category> _categoryRepo;
		private readonly IGenericRepository<SysFile> _sysFileRepo;
		private readonly JwtIssuerOptions _jwtIssuerOptions;
		private readonly IConfiguration _configuration;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly string GlobalUserName;
		private readonly IHttpContextAccessor _contextAccessor;


		private readonly IMapper _mapper;
		private static DateTime lastApiCallTime = DateTime.MinValue;
		private readonly UserManager<ApplicationUser> _userManager;
		public ArticleService(IRepository<Article> articleRepo,
			IMapper mapper, UserManager<ApplicationUser> userManager,
			IRepository<Category> category,
			IGenericRepository<SysFile> sysFile,
			IHttpContextAccessor httpContextAccessor,
			IConfiguration configuration,
			IOptions<JwtIssuerOptions> jwtIssuerOptions,
			IHttpContextAccessor contextAccessor) : base(articleRepo, mapper)
		{
			_articleRepo = articleRepo;
			_mapper = mapper;
			_userManager = userManager;
			_categoryRepo = category;
			_sysFileRepo = sysFile;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_jwtIssuerOptions = jwtIssuerOptions.Value;
			_contextAccessor = contextAccessor;
			GlobalUserName = _contextAccessor.HttpContext.User.Identity.IsAuthenticated ? _contextAccessor.HttpContext.User.Identity.Name : string.Empty;
		}

		public async Task<ResponseResult> GetAllArticlesByCategoryId(long categoryId)
		{
			try
			{
				var result = await _articleRepo.GetAllPagination(1, 10,
					where: e => EF.Property<long?>(e, "CategoryId") == categoryId, orderDesc: e => e.CreatedDate);
				if (result.Records.Any())
				{
					return new ResponseResult
					{
						Data = result.Records,
						Success = true,
					};
				}
				else
				{
					return new ResponseResult
					{
						Message = MsgConstants.WarningMessages.NotFoundData,
						Success = false,
					};
				}
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				return new ResponseResult
				{
					Message = message,
					Success = false,
				};
			}
		}

		//Getbyid and Plus CountView

		public override async Task<ResponseResult> GetById(long id)
		{
			var result = new ResponseResult();
			try
			{
				var article = await _articleRepo.GetById(id);

				if (article != null && article.IsActive.HasValue)
				{
					GetByIdEntry(article);
					article.CountView += 1;
					await _articleRepo.Update(article);

					var user = article.UserId != null ? await _userManager.FindByIdAsync(article.UserId) : null;
					var fileIds = new List<long?>
					{
						article.ThumbnailFileId
					}.Where(id => id.HasValue).Select(id => id.Value).ToList();

					var sysFileAttachment = article.AttachmentFileId != null ? await _sysFileRepo.GetById(article.AttachmentFileId.Value) : null;
					var sysFileThumbnail = article.ThumbnailFileId != null ? await _sysFileRepo.GetById(article.ThumbnailFileId.Value) : null;

					var articleViewModel = _mapper.Map<Article, ArticleGetByIdVModel>(article);
					var sysFileViewModel = sysFileAttachment != null ? _mapper.Map<SysFile, SysFileGetByIdVModel>(sysFileAttachment) : null;
					var sysFileViewModel2 = sysFileThumbnail != null ? _mapper.Map<SysFile, SysFileGetByIdVModel>(sysFileThumbnail) : null;
					var files = await _sysFileRepo.Where(x => fileIds.Contains(x.Id));

					SysFile thumbnailFile = files.FirstOrDefault(f => f.Id == article.ThumbnailFileId);

					articleViewModel.AttachmentFileInfo = sysFileViewModel;
					articleViewModel.ThumbnailFileInfo = sysFileViewModel2;
					articleViewModel.ThumbnailURL = thumbnailFile != null ? $"{_jwtIssuerOptions.Audience}{thumbnailFile?.Path}" : null;
					articleViewModel.AuthorName = user?.UserName;

					if (articleViewModel.AttachmentFileInfo != null)
					{
						articleViewModel.AttachmentFileInfo.Path = $"{GetBaseUrl()}{articleViewModel.AttachmentFileInfo.Path}";
					}
					if (articleViewModel.ThumbnailFileInfo != null)
					{
						articleViewModel.ThumbnailFileInfo.Path = $"{GetBaseUrl()}{articleViewModel.ThumbnailFileInfo.Path}";
					}
					result.Data = articleViewModel;
					result.Success = true;

					await _articleRepo.SaveChanges(result);
				}
				else
				{
					result.Success = false;
					result.Message = article != null ? "SysFile not found" : MsgConstants.WarningMessages.NotFoundData;
				}
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		private string GetBaseUrl()
		{
			var request = _httpContextAccessor.HttpContext.Request;
			return $"{request.Scheme}://{request.Host}";
		}

		//GetAll and Sorted Article
		public override async Task<ResponseResult> GetAll(FiltersGetAllVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _articleRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					BuildPredicate(model),
					BuildOrderBy(model)

				);
				var recordsWithUserDetails = new List<ArticleWithUserDetails>();
				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
					var user = await _userManager.FindByIdAsync(entity.UserId);
					Category category = null;
					SysFile sysfile = null;
					if (entity.CategoryId != null)
					{
						category = await _categoryRepo.GetById(entity.CategoryId);
					}
					if (entity.ThumbnailFileId != null)
					{
						sysfile = await _sysFileRepo.GetById(entity.ThumbnailFileId);
					}
					var recordWithUserDetails = new ArticleWithUserDetails
					{
						Article = _mapper.Map<Article, ArticleGetAllVModel>(entity),

					};
					if (sysfile != null)
					{
						// Set "ThumbnailURL" only if sysfile and urls are not null
						recordWithUserDetails.Article.ThumbnailURL = $"{_jwtIssuerOptions.Audience}{sysfile.Path}";
					}
					else
					{
						// Fallback to just the sysfile.Path if sysfile or urls is null
						recordWithUserDetails.Article.ThumbnailURL = sysfile?.Path;
					}

					recordsWithUserDetails.Add(recordWithUserDetails);
				}
				//data.Records = data.Records.Select(r => _mapper.Map<Article, ArticleGetAllVModel>(r));
				if (!model.IsExport)
				{
					//data.Records = recordsWithUserDetails;
					data.Records = data.Records.Select(r => _mapper.Map<Article, ArticleGetAllVModel>(r));
				}
				result.Data = data;
				result.Success = true;
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		public async Task<ResponseResult> GetAllExtra(FilterGetAllExtraVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _articleRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					BuildPredicateExtra(model),
					BuildOrderBy(model)

				);
				var recordsWithUserDetails = new List<ArticleWithUserDetails>();
				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
					var user = await _userManager.FindByIdAsync(entity.UserId);
					Category category = null;
					SysFile sysfile = null;
					if (entity.CategoryId != null)
					{
						category = await _categoryRepo.GetById(entity.CategoryId);
					}
					if (entity.ThumbnailFileId != null)
					{
						sysfile = await _sysFileRepo.GetById(entity.ThumbnailFileId);
					}
					var recordWithUserDetails = new ArticleWithUserDetails
					{
						Article = _mapper.Map<Article, ArticleGetAllVModel>(entity),

					};
					if (sysfile != null)
					{
						recordWithUserDetails.Article.ThumbnailURL = $"{_jwtIssuerOptions.Audience}{sysfile.Path}";
					}
					else
					{
						recordWithUserDetails.Article.ThumbnailURL = sysfile?.Path;
					}

					recordsWithUserDetails.Add(recordWithUserDetails);
				}
				if (!model.IsExport)
				{
					data.Records = data.Records.Select(r => _mapper.Map<Article, ArticleGetAllVModel>(r));
				}
				result.Data = data;
				result.Success = true;
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		//GetALL
		private Expression<Func<Article, bool>> BuildPredicate(FiltersGetAllVModel model)
		{
			return x =>
				(model.IsActive == null || x.IsActive == model.IsActive) &&
				(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
				(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
				(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
				(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy);
		}
		private Expression<Func<Article, bool>> BuildPredicateExtra(FilterGetAllExtraVModel model)
		{
			return x =>
				(model.IsActive == null || x.IsActive == model.IsActive) &&
				(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
				(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
				(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
				(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy) &&
				(model.CategoryId == null || x.CategoryId == model.CategoryId);
		}

		private Expression<Func<Article, dynamic>> BuildOrderBy(FiltersGetAllVModel model)
		{
			switch (model.SortBy?.ToLower())
			{
				case "countview":
					return c => c.CountView;
				case "ishighlight":
					return c => c.IsHighlight;
				case "name":
					return c => c.Name;
				default:
					return c => c.Id;
			}
		}


		public virtual async Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _articleRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					x =>
						(model.IsActive == null || x.IsActive == model.IsActive) &&
						(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
						(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
						(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
						(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy),
					BuildOrderBy(model)
				);

				var recordsWithUserDetails = new List<ArticleWithUserDetails>();

				foreach (var entity in data.Records)
				{
					// 1. Load user details
					GetAllEntry(entity);
					var user = await _userManager.FindByIdAsync(entity.UserId);
					// 2. Load category details
					Category category = null;
					if (entity.CategoryId != null)
					{
						category = await _categoryRepo.GetById(entity.CategoryId);
					}
					// 3. Load sysfile details
					SysFile sysfile = null;
					if (entity.ThumbnailFileId != null)
					{
						sysfile = await _sysFileRepo.GetById(entity.ThumbnailFileId);
					}
					// 4. Create record with user details
					var recordWithUser = new ArticleWithUserDetails
					{
						Article = _mapper.Map<Article, ArticleGetAllVModel>(entity),
					};
					// 5. Set ThumbnailURL if sysfile is not null
					if (sysfile != null)
					{
						recordWithUser.Article.ThumbnailURL = sysfile.Path;
					}
					// 6. Add the record to the list
					recordsWithUserDetails.Add(recordWithUser);
				}
				var list = data.Records
					.Where(r => string.IsNullOrEmpty(keyword) ||
						(r.Name?.ToLower()?.Contains(keyword.ToLower()) == true) ||
						(r.Alias?.ToLower()?.Contains(Utilities.SlugGenerator.GenerateAliasFromName(keyword.ToLower())) == true) ||
						(r.ShortDescription?.ToLower()?.Contains(keyword.ToLower()) == true) ||
						(r.Description?.ToLower()?.Contains(keyword.ToLower()) == true) ||
						(r.User != null && (r.User.FirstName.ToLower().Contains(keyword.ToLower()) || r.User.LastName.ToLower().Contains(keyword.ToLower()))) ||
						(r.Category != null && r.Category.Name.ToLower().Contains(keyword.ToLower()))
					)
					.Select(r => _mapper.Map<Article, ArticleGetAllVModel>(r));

				if (!model.IsDescending)
				{
					data.Records = string.IsNullOrEmpty(model.SortBy)
						? list.OrderBy(r => r.Id).ToList()
						: list.OrderBy(r => r.GetType().GetProperty(model.SortBy)?.GetValue(r, null)).ToList();
				}
				else
				{
					data.Records = string.IsNullOrEmpty(model.SortBy)
						? list.OrderByDescending(r => r.Id).ToList()
						: list.OrderByDescending(r => r.GetType().GetProperty(model.SortBy)?.GetValue(r, null)).ToList();
				}

				data.TotalRecords = data.Records.Count();
				result.Data = data;
				result.Success = true;
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		public override async Task<ResponseResult> Update(ArticleUpdateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entity = await _articleRepo.GetById(((dynamic)model).Id);
				if (entity != null)
				{
					entity = _mapper.Map(model, entity);

					entity.UpdatedBy = GlobalUserName;

					result = await _articleRepo.Update(entity);
				}
				else
				{
					result.Success = false;
					result.Message = MsgConstants.WarningMessages.NotFoundData;
				}
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		public ResponseResult GetAllByQueryString(FiltersGetAllByQueryStringVModel model)
		{
			throw new NotImplementedException();
		}
	}
}
