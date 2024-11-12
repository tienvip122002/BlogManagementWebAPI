using AutoMapper;
using BlogManagement.Data.Abstract;
using BlogManagement.Domain.Constants;
using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using BlogManagement.Service.Abstract;
using BlogManagement.Service.Abstract.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogManagement.Service
{
    public class BaseService<TEntity, TCreateVModel, TUpdateVModel, TGetByIdVModel, TGetAllVModel, TExport> 
		where TEntity : BaseEntitie
		where TGetByIdVModel : class
		where TExport : class
	{
		private readonly IRepository<TEntity> _repository;
		private readonly IMapper _mapper;
		public BaseService(IRepository<TEntity> repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}
		public virtual async Task<ResponseResult> GetAll(FiltersGetAllVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _repository.GetAllPagination(
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

				foreach (var entity in data.Records)
				{
					GetAllEntry(entity);
				}
				if (!model.IsExport)
					data.Records = data.Records.Select(r => _mapper.Map<TEntity, TGetAllVModel>(r));
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

		public virtual async Task<ResponseResult> GetById(long id)
		{
			var result = new ResponseResult();
			try
			{
				var entity = await _repository.GetById(id);
				if (entity != null && entity.IsActive.HasValue)
				{
					GetByIdEntry(entity);
					result.Data = _mapper.Map<TEntity, TGetByIdVModel>(entity);
					result.Success = true;
				}
				else
				{
					result.Success = false;
					result.Message = MsgConstants.WarningMessages.NotFoundData;
				};
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}
		public virtual async Task<ResponseResult> Create(TCreateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entityCreated = _mapper.Map<TCreateVModel, TEntity>(model);
				result = await _repository.Create(entityCreated);
				result.Data = _mapper.Map<TEntity, TGetByIdVModel>(result.Data);
			}
			catch (Exception ex)
			{
				var message = Utilities.MakeExceptionMessage(ex);
				result.Message = message;
				result.Success = false;
			}
			return result;
		}
		public virtual async Task<ResponseResult> Update(TUpdateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entity = await _repository.GetById(((dynamic)model).Id);
				if (entity != null)
				{
					entity = _mapper.Map(model, entity);
					result = await _repository.Update(entity);
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
		public virtual async Task<ResponseResult> UpdateMany(IEnumerable<TUpdateVModel> models)
		{
			var result = new ResponseResult();
			try
			{
				foreach (var model in models)
				{
					var entity = await _repository.GetById(((dynamic)model).Id);
					entity = _mapper.Map(model, entity);
					result = await _repository.Update(entity);
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
		public virtual async Task<ResponseResult> ChangeStatus(long id)
		{
			var result = new ResponseResult();
			try
			{
				var items = await _repository.GetById(id);
				if (items != null)
				{
					items.IsActive = !items.IsActive;
					result = await _repository.Update(items);
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

		public virtual async Task<ResponseResult> Remove(long id)
		{
			var result = new ResponseResult();
			try
			{
				TEntity entity = await _repository.GetById(id);
				if (entity != null)
				{
					result = await _repository.Remove(entity.Id);
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
		public virtual void GetAllEntry(TEntity entity)
		{
			//_repository.EntryReference(entity, x => x.LanguageId);
			// override this function in child class if needed
		}
		public virtual void GetByIdEntry(TEntity entity)
		{
			//_repository.EntryReference(entity, x => x.LanguageId);
			// override this function in child class if needed
		}

		public virtual async Task<ExportStream> ExportFile(FiltersGetAllVModel model, ExportFileVModel exportModel)
		{
			model.IsExport = true;
			var result = await GetAll(model);
			foreach (var entity in result.Data.Records)
			{
				if (entity != null)
					GetAllEntry(entity);
			}
			var records = _mapper.Map<IEnumerable<TEntity>, IEnumerable<TExport>>(result.Data.Records);
			var exportData = ImportExportHelper<TExport>.ExportFile(exportModel, records);
			return exportData;
		}
	}
}
