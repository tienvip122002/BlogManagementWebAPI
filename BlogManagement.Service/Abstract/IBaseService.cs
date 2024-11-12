using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
    public interface IBaseService<TEntity, TCreateVModel, TUpdateVModel, TGetByIdVModel, TGetAllVModel> where TEntity : BaseEntitie
    {
        Task<ResponseResult> ChangeStatus(long id);
        Task<ResponseResult> Create(TCreateVModel model);
        Task<ExportStream> ExportFile(FiltersGetAllVModel model, ExportFileVModel exportModel);
        Task<ResponseResult> GetAll(FiltersGetAllVModel model);
        Task<ResponseResult> GetById(long id);
        Task<ResponseResult> Remove(long id);
        Task<ResponseResult> Update(TUpdateVModel model);
        Task<ResponseResult> UpdateMany(IEnumerable<TUpdateVModel> models);
    }
}