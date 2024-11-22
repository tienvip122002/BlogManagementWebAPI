using AutoMapper;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Configurations;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
	public class CategoryService : BaseService<Category, CategoryCreateVModel, CategoryUpdateVModel, CategoryGetByIdVModel, CategoryGetAllVModel, CategoryExport>, ICategoryService
	{
		private readonly IRepository<Category> _categoryRepo;
		private readonly IGenericRepository<SysFile> _sysFileRepo;
		private readonly JwtIssuerOptions _jwtIssuerOptions;
		private readonly IMapper _mapper;
		public CategoryService(IRepository<Category> categoryRepo, IGenericRepository<SysFile> sysFileRepo, IOptions<JwtIssuerOptions> jwtIssuerOptions, IMapper mapper) : base(categoryRepo, mapper)
		{
			_categoryRepo = categoryRepo;
			_sysFileRepo = sysFileRepo;
			_jwtIssuerOptions = jwtIssuerOptions.Value;
			_mapper = mapper;
		}

		public async Task<ResponseResult> GetAllAsTree(FiltersGetAllVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _categoryRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					x =>
					(model.IsActive == null || x.IsActive == model.IsActive) &&
					(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
					(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy)
					,
					x => x.Id
					);
				var resultList = new List<CategoryGetAllAsTreeVModel>();
				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
					SysFile thumbnailFile = entity.ThumbnailFileId != null ? await _sysFileRepo.GetById(entity.ThumbnailFileId) : null;
					var remappedModel = _mapper.Map<Category, CategoryGetAllAsTreeVModel>(entity);
					remappedModel.ThumbnailFileURL = thumbnailFile != null ? $"{_jwtIssuerOptions.Audience}{thumbnailFile?.Path}" : null;

					resultList.Add(remappedModel);
				}
				if (!model.IsExport)
					if (model.IsDescending != true)
						data.Records = HandleRecursive(resultList).OrderBy(r => r.Sort);
					else
						data.Records = HandleRecursive(resultList).OrderByDescending(r => r.Sort);
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

		public override async Task<ResponseResult> GetAll(FiltersGetAllVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _categoryRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					x =>
					(model.IsActive == null || x.IsActive == model.IsActive) &&
					(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
					(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy)
					,
					x => x.Id
					);
				var resultList = new List<CategoryGetAllVModel>();
				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
					SysFile thumbnailFile = entity.ThumbnailFileId != null ? await _sysFileRepo.GetById(entity.ThumbnailFileId) : null;
					var remappedModel = _mapper.Map<Category, CategoryGetAllVModel>(entity);
					remappedModel.ThumbnailFileURL = thumbnailFile != null ? $"{_jwtIssuerOptions.Audience}{thumbnailFile?.Path}" : null;

					resultList.Add(remappedModel);
				}
				if (!model.IsExport)
					if (!model.IsDescending)
					{
						if (!string.IsNullOrEmpty(model.SortBy))
						{
							data.Records = resultList
								.OrderBy(x => x.GetType().GetProperty(model.SortBy)?.GetValue(x, null));
						}
						else data.Records = resultList.OrderBy(r => r.Id);
					}
					else
					{
						data.Records = resultList.OrderByDescending(x => x.Id);
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

		public async Task<ResponseResult> Search(FiltersGetAllVModel model, string keyword)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _categoryRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					x =>
					(model.IsActive == null || x.IsActive == model.IsActive) &&
					(string.IsNullOrEmpty(model.CreatedDate.ToString()) || (x.CreatedDate.Value.Year.Equals(model.CreatedDate.Value.Year) && x.CreatedDate.Value.Month.Equals(model.CreatedDate.Value.Month) && x.CreatedDate.Value.Day.Equals(model.CreatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.CreatedBy) || x.CreatedBy == model.CreatedBy) &&
					(string.IsNullOrEmpty(model.UpdatedDate.ToString()) || (x.UpdatedDate.Value.Year.Equals(model.UpdatedDate.Value.Year) && x.UpdatedDate.Value.Month.Equals(model.UpdatedDate.Value.Month) && x.UpdatedDate.Value.Day.Equals(model.UpdatedDate.Value.Day))) &&
					(string.IsNullOrEmpty(model.UpdatedBy) || x.UpdatedBy == model.UpdatedBy)
					,
					x => x.Id
					);

				var resultList = new List<CategoryGetAllVModel>();
				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
					SysFile thumbnailFile = entity.ThumbnailFileId != null ? await _sysFileRepo.GetById(entity.ThumbnailFileId) : null;
					var remappedModel = _mapper.Map<Category, CategoryGetAllVModel>(entity);
					//remappedModel.ThumbnailFilePath = thumbnailFile != null ? $"{_jwtIssuerOptions.Audience}{thumbnailFile?.Path}" : null;

					resultList.Add(remappedModel);
				}
				data.Records = resultList;
				if (!model.IsExport)
					if (!model.IsDescending)
					{
						if (!string.IsNullOrEmpty(model.SortBy))
						{
							data.Records = data.Records
								.Where(x => x.Name.ToLower().Contains(keyword.ToLower()))
								.OrderBy(x => x.GetType().GetProperty(model.SortBy)?.GetValue(x, null));
						}
						else data.Records = data.Records
								.Where(r => r.Name.ToLower().Contains(keyword.ToLower()))
								.OrderBy(r => r.Id);
					}
					else
					{
						data.Records = data.Records
								.Where(x => x.Name.ToLower().Contains(keyword.ToLower()))
								.OrderByDescending(r => r.Id);
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

		public override async Task<ResponseResult> Create(CategoryCreateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entityCreated = _mapper.Map<CategoryCreateVModel, Category>(model);
				var entity = await _categoryRepo.Where(x => x.Name == model.Name);
				if (entity.Count() > 0)
				{
					result.Message = "Name already exists.";
					result.Success = false;
					return result;
				}
				entityCreated.Alias = Utilities.SlugGenerator.GenerateAliasFromName(entityCreated.Name);
				result = await _categoryRepo.Create(entityCreated);
				result.Data = _mapper.Map<Category, CategoryGetByIdVModel>(result.Data);
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}

		public IEnumerable<dynamic> HandleRecursive(IEnumerable<dynamic> records)
		{
			var parentRecords = new List<CategoryGetAllAsTreeVModel>();
			var recordDict = records.ToDictionary(r => (long)r.Id, r => r);

			foreach (CategoryGetAllAsTreeVModel item in records)
			{
				if (item.ParentId == null)
				{
					parentRecords.Add(recordDict[(long)item.Id]);
					recordDict[(long)item.Id].Children = GetChilds((IEnumerable<dynamic>)records, item, recordDict);
				}
			}
			return parentRecords;
		}

		public List<CategoryGetAllAsTreeVModel> GetChilds(IEnumerable<dynamic> nodes, CategoryGetAllAsTreeVModel parentNode, Dictionary<long, dynamic> recordDict)
		{
			var newRecords = new List<CategoryGetAllAsTreeVModel>();
			try
			{
				var childs = nodes.Where(item => item.ParentId == parentNode.Id).ToList();
				foreach (var child in childs)
				{
					recordDict[(long)child.Id].Children = GetChilds(nodes, child, recordDict);
					newRecords.Add(recordDict[(long)child.Id]);
				}
			}
			catch (Exception)
			{
				return newRecords;
			}
			return newRecords;
		}
		public override async Task<ResponseResult> Update(CategoryUpdateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entity = await _categoryRepo.GetById(((dynamic)model).Id);
				if (entity != null)
				{
					if (model.Name != entity.Name)
					{
						var name = await _categoryRepo.Where(x => x.Name == model.Name);
						if (name.Count() > 0)
						{
							result.Message = "Name already exists.";
							result.Success = false;
							return result;
						}
					}
					entity.Name = model.Name;
					entity = _mapper.Map(model, entity);
					result = await _categoryRepo.Update(entity);
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
	}
}
