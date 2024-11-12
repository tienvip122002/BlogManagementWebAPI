using BlogManagement.Domain.Entities;
using BlogManagement.Domain.Model;
using BlogManagement.Domain.VModels;
using System.Threading.Tasks;

namespace BlogManagement.Service.Abstract
{
    public interface ISysFileService
    {
        Task<ResponseResult> ChangeStatus(long id);
        Task<ResponseResult> Create(SysFileCreateVModel model);
        Task<ResponseResult> CreateBase64(SysFileCreateBase64VModel model);
        Task<ResponseResult> FileChunks(FileChunk fileChunk);
        Task<ResponseResult> GetAll(SysFileGetAllVModel model);
        Task<ResponseResult> GetAllByType(string fileType, int pageSize, int pageNumber);
        void GetAllEntry(SysFile entity);
        Task<ResponseResult> Remove(long id);
        Task<ResponseResult> RemoveByUrl(string url);
        Task<ResponseResult> Update(SysFileUpdateVModel model);
        Task<ResponseResult> UploadAvatar(FileChunk fileChunk);
    }
}