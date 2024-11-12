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
using BlogManagement.Domain.Model;
using Microsoft.Extensions.Options;
using static BlogManagement.Domain.Constants.CommonConstants;
using static System.Net.Mime.MediaTypeNames;
using System.Globalization;
using System.IO;
using System.Linq.Expressions;
using System.Web;
using BlogManagement.Data;
using BlogManagement.Domain.VModels;
using static BlogManagement.Domain.Constants.MsgConstants;
using BlogManagement.Service.Abstract.Helpers;
using System.Drawing;
using System.Drawing.Imaging;
using BlogManagement.Service.Abstract;

namespace BlogManagement.Service
{
    public class SysFileService : GlobalVariables, ISysFileService
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
		public SysFileService(IGenericRepository<SysFile> sysfileRepo,
		   IMapper mapper,
		   IHttpContextAccessor contextAccessor,
		   IOptions<UploadConfigurations> uploadConfigs,
		   IWebHostEnvironment env,
		   IOptions<JwtIssuerOptions> jwtIssuerOptions,
		   IHttpContextAccessor httpContextAccessor) : base(contextAccessor)
		{
			_sysfileRepo = sysfileRepo;
			_mapper = mapper;
			_jwtIssuerOptions = jwtIssuerOptions.Value;
			_uploadConfigs = uploadConfigs.Value;
			_env = env;
			_tempFolder = uploadConfigs.Value.TempFolder;
			_chunkSize = 1048576 * _uploadConfigs.ChunkSize;
			_tempPath = _env.WebRootPath + "/" + _uploadConfigs.FileUrl + _tempFolder;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<ResponseResult> GetAll(SysFileGetAllVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var data = await _sysfileRepo.GetAllPagination(
					model.PageNumber,
					model.PageSize,
					BuildPredicate(model),
					BuildOrderBy(model)
				);
				foreach (var entity in data.Records)
				{
					if (!string.IsNullOrEmpty(entity.Path) && !entity.Path.StartsWith(_jwtIssuerOptions.Audience, StringComparison.OrdinalIgnoreCase))
					{
						entity.Path = $"{_jwtIssuerOptions.Audience.TrimEnd('/')}{entity.Path}";
					}
				}
				var sysFileList = _mapper.Map<IEnumerable<SysFile>, IEnumerable<SysFileGetByIdVModel>>((IEnumerable<SysFile>)data.Records);
				result.Data = sysFileList;
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

		private Expression<Func<SysFile, bool>> BuildPredicate(SysFileGetAllVModel model)
		{
			return m =>
			  (string.IsNullOrEmpty(model.Name) || m.Name.Contains(model.Name)) &&
			  (string.IsNullOrEmpty(model.CreatedBy) || m.CreatedBy == model.CreatedBy) &&
			  (string.IsNullOrEmpty(model.UpdatedBy) || m.UpdatedBy == model.UpdatedBy) &&
			  (string.IsNullOrEmpty(model.Path) || m.Path == model.Path) &&
			  (model.IsActive == null || m.IsActive == model.IsActive) &&
			  (string.IsNullOrEmpty(model.Type) || m.Type == model.Type) &&
			  (model.Id == 0 || m.Id == model.Id);
		}


		private Expression<Func<SysFile, dynamic>> BuildOrderBy(SysFileGetAllVModel model)
		{
			Expression<Func<SysFile, dynamic>> orderBy = x => x.Id;

			if (!string.IsNullOrEmpty(model.SortBy))
			{
				switch (model.SortBy.ToLower())
				{
					case "name":
						orderBy = x => x.Name;
						break;
					case "path":
						orderBy = x => x.Path;
						break;
					case "type":
						orderBy = x => x.Type;
						break;
				}
			}
			return orderBy;
		}

		//public async Task<ResponseResult> Create(SysFileCreateVModel model)
		//{
		//    var result = new ResponseResult();
		//    model.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name);
		//    model.Type = _medias.Contains(model.Type) ? model.Type : CommonConstants.FileType.Other;
		//    string yyyy = DateTime.Now.ToString("yyyy");
		//    string mm = DateTime.Now.ToString("MM");
		//    string envPath = _uploadConfigs.FileUrl + "/" + yyyy + "/" + mm;
		//    if (!Directory.Exists(_env.WebRootPath + "/" + envPath))
		//        Directory.CreateDirectory(_env.WebRootPath + "/" + envPath);
		//    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(model.Name);
		//    string fileExtension = Path.GetExtension(model.Name);
		//    string newFilePath = _env.WebRootPath + "/" + envPath + "/" + fileNameWithoutExtension + fileExtension;
		//    int count = 1;
		//    try
		//    {
		//        while (File.Exists(newFilePath))
		//        {
		//            fileNameWithoutExtension = $"{Path.GetFileNameWithoutExtension(model.Name)}_{count}";
		//            newFilePath = _env.WebRootPath + "/" + envPath + "/" + fileNameWithoutExtension + fileExtension;
		//            count++;
		//        }
		//        File.Move(_tempPath + "/" + model.Name, newFilePath);
		//        model.Path = $"/{envPath}/{Path.GetFileName(newFilePath)}";
		//        SysFile aspSystemFile = Utilities.ConvertModel<SysFile>(model);
		//        result = await _sysfileRepo.Create(aspSystemFile);
		//        aspSystemFile.CreatedDate = DateTime.Now;
		//        result.Data = Utilities.ConvertModel<SysFileCreateVModel>(result.Data);
		//        result.Data.Path = $"{GetBaseUrl()}{result.Data.Path}";
		//    }
		//    catch (Exception ex)
		//    {
		//        var message = Utilities.MakeExceptionMessage(ex);
		//        result.Message = message;
		//        result.Success = false;
		//    }
		//    return result;
		//}
		public async Task<ResponseResult> Create(SysFileCreateVModel model)
		{
			var result = new ResponseResult();
			model.Name = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(model.Name);

			string fileExtension = Path.GetExtension(model.Name).TrimStart('.').ToLower();
			switch (fileExtension)
			{
				case "zip":
					model.Type = FileType.Zip;
					break;
				case "rar":
					model.Type = FileType.Rar;
					break;
				case "mp3":
				case "wav":
				case "ogg":
					model.Type = FileType.Audio;
					break;
				case "mp4":
				case "avi":
					model.Type = FileType.Video;
					break;
				case "doc":
				case "docx":
				case "pdf":
					model.Type = FileType.Document;
					break;
				case "png":
				case "jpg":
				case "jpeg":
				case "gif":
				case "bmp":
					model.Type = FileType.Image;
					break;
				default:
					model.Type = FileType.Other;
					break;
			}

			string yyyy = DateTime.Now.ToString("yyyy");
			string mm = DateTime.Now.ToString("MM");
			string envPath = $"{_uploadConfigs.FileUrl}/{yyyy}/{mm}";

			// Kiểm tra và tạo thư mục nếu nó không tồn tại
			if (!Directory.Exists($"{_env.WebRootPath}/{envPath}"))
				Directory.CreateDirectory($"{_env.WebRootPath}/{envPath}");

			string pathDoc = $"{envPath}/{model.Name}";

			try
			{
				// Đường dẫn mới cho file sau khi ghép các chunk
				string newPath = Path.Combine(_tempPath, model.Name);

				// Lấy danh sách các file chunk và sắp xếp chúng
				string[] filePaths = Directory.GetFiles(_tempPath)
					.Where(p => p.Contains(model.Name))
					.OrderBy(p => int.Parse(p.Replace(model.Name, "$").Split('$')[1])).ToArray();

				// Ghép các chunk vào file mới
				foreach (string filePath in filePaths)
				{
					MergeChunks(newPath, filePath);
				}

				// Di chuyển file đã ghép vào thư mục đích
				File.Move(newPath, $"{_env.WebRootPath}/{pathDoc}");

				// Cập nhật đường dẫn file trong model
				model.Path = $"/{pathDoc}";
				SysFile aspSystemFile = Utilities.ConvertModel<SysFile>(model);

				// Thêm file vào cơ sở dữ liệu
				result = await _sysfileRepo.Create(aspSystemFile);
				result.Data = Utilities.ConvertModel<SysFileCreateVModel>(result.Data);
			}
			catch (Exception ex)
			{
				// Xử lý ngoại lệ và cập nhật thông tin phản hồi
				result.Message = ex.Message;
				result.Success = false;
			}

			return result;
		}


		public async Task<ResponseResult> FileChunks(FileChunk fileChunk)
		{
			var result = new ResponseResult();
			try
			{
				if (Directory.Exists(_tempPath) == false)
					Directory.CreateDirectory(_tempPath);

				string newpath = _tempPath + "/" + fileChunk.FileName;
				using (FileStream fs = File.Create(newpath))
				{
					byte[] bytes = new byte[_chunkSize];
					int bytesRead = 0;
					if ((bytesRead = await fileChunk.File.OpenReadStream().ReadAsync(bytes, 0, bytes.Length)) > 0)
						fs.Write(bytes, 0, bytesRead);
				}
				result.Success = true;
			}
			catch (Exception ex)
			{
				result.Message = ex.Message;
				result.Success = false;
			}
			return result;
		}

		private void MergeChunks(string chunk1, string chunk2)
		{
			FileStream fs1 = null;
			FileStream fs2 = null;
			try
			{
				fs1 = File.Open(chunk1, FileMode.Append);
				fs2 = File.Open(chunk2, FileMode.Open);
				byte[] fs2Content = new byte[fs2.Length];
				fs2.Read(fs2Content, 0, (int)fs2.Length);
				fs1.Write(fs2Content, 0, (int)fs2.Length);
			}
			catch (Exception ex)
			{
				//logger
				throw new Exception($"Error merging chunks: {ex.Message}", ex);
			}
			finally
			{
				fs1?.Close();
				fs2?.Close();
				File.Delete(chunk2);
			}
		}

		public async Task<ResponseResult> Update(SysFileUpdateVModel model)
		{
			var result = new ResponseResult();
			try
			{
				var entity = await _sysfileRepo.GetById(model.Id);
				if (entity != null)
				{
					// Tạo entity từ model
					entity = _mapper.Map(model, entity);
					var baseUrl = _jwtIssuerOptions.Audience.TrimEnd('/'); // Chắc chắn Base URL kết thúc bằng '/'
					var path = entity.Path;
					if (!string.IsNullOrEmpty(path))
					{
						Uri uri;
						if (Uri.TryCreate(path, UriKind.Absolute, out uri))
						{
							entity.Path = uri.PathAndQuery;
						}
					}

					result = await _sysfileRepo.Update(entity);
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

		public async Task<ResponseResult> CreateBase64(SysFileCreateBase64VModel model)
		{
			string path = string.Empty;
			string type = string.Empty;
			if (!string.IsNullOrEmpty(model.Base64String))
			{
				string name = model.Name;
				var (uploadConfigs, typeOf) = ConvertBase64String.ConvertBase64ToImage(model.Base64String, $"{_env.WebRootPath}/{_uploadConfigs.ImageUrl}", name);
				path = uploadConfigs;
				type = typeOf;
			}
			// Map SysFileCreateBase64VModel sang SysFile
			var createdEntityBase64String = _mapper.Map<SysFileCreateBase64VModel, SysFile>(model);
			// Lấy sub domain www.local/upload/abc -> /upload/abc
			createdEntityBase64String.Path = path.Replace(_env.WebRootPath, "");
			createdEntityBase64String.Type = type?.Trim();
			createdEntityBase64String.CreatedDate = DateTime.Now;
			// Thêm vào repo
			var result = await _sysfileRepo.Create(createdEntityBase64String);
			// Chuyển sang GetById để trả về đối tượng
			result.Data = _mapper.Map<SysFile, SysFileGetByIdVModel>(result.Data);
			result.Data.Path = $"{GetBaseUrl()}{result.Data.Path}";
			return result;
		}

		private string GetBaseUrl()
		{
			var request = _httpContextAccessor.HttpContext.Request;
			return $"{request.Scheme}://{request.Host}";
		}

		public async Task<ResponseResult> RemoveByUrl(string url)
		{
			ResponseResult result = new ResponseResult();
			url = HttpUtility.UrlDecode(url);
			string urlRemovedDomain = url.Replace(_jwtIssuerOptions.Audience, "");
			var entity = _sysfileRepo.AsQueryable().FirstOrDefault(x => x.Path.Contains(urlRemovedDomain));
			if (entity != null)
			{
				string path = $"{_env.WebRootPath}/{entity.Path.Replace(_jwtIssuerOptions.Audience, "")}";
				if (File.Exists(path))
					File.Delete(path);
				result = await _sysfileRepo.Remove(entity.Id);
				result.Success = true;
			}
			else
			{
				result.Success = false;
				result.Message = WarningMessages.NotFoundData;
			};

			return result;
		}

		public async Task<ResponseResult> ChangeStatus(long id)
		{
			var result = new ResponseResult();
			try
			{
				var sysFile = await _sysfileRepo.GetById(id);
				if (sysFile != null)
				{
					sysFile.UpdatedDate = DateTime.Now;
					sysFile.IsActive = !sysFile.IsActive;
					sysFile.UpdatedBy = GlobalUserName;
					var updateResult = await _sysfileRepo.Update(sysFile);
					if (updateResult.Success)
					{
						result.Success = true;
						result.Data = updateResult;
					}
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

		public async Task<ResponseResult> Remove(long id)
		{
			ResponseResult result = new ResponseResult();
			SysFile entity = await _sysfileRepo.GetById(id);
			if (entity != null)
			{
				string path = $"{_env.WebRootPath}/{entity.Path.Replace(_jwtIssuerOptions.Audience, "")}";
				if (File.Exists(path))
					File.Delete(path);
				result = await _sysfileRepo.Remove(entity.Id);
				result.Success = true;
			}
			else
			{
				result.Success = false;
				result.Message = MsgConstants.WarningMessages.NotFoundData;
			};

			return result;
		}


		private ImageFormat GetImageFormat(string path)
		{
			using (FileStream fs = new FileStream(path, FileMode.Open, FileAccess.Read))
			{
				using (System.Drawing.Image image = System.Drawing.Image.FromStream(fs, false, false))
				{
					return image.RawFormat;
				}
			}
		}

		public async Task<ResponseResult> UploadAvatar(FileChunk fileChunk)
		{
			var responseData = new ResponseResult();
			try
			{
				if (!Directory.Exists(_tempPath))
					Directory.CreateDirectory(_tempPath);

				FileInfo fi = new FileInfo(fileChunk.FileName);
				fileChunk.FileName = string.Format("{0}{1}", Guid.NewGuid().ToString(), fi.Extension);
				string newpath = Path.Combine(_tempPath, fileChunk.FileName);

				using (FileStream fs = File.Create(newpath))
				{
					byte[] bytes = new byte[_chunkSize];
					int bytesRead = 0;

					if ((bytesRead = await fileChunk.File.OpenReadStream().ReadAsync(bytes, 0, bytes.Length)) > 0)
					{
						fs.Write(bytes, 0, bytesRead);
					}
				}

				ImageFormat imageFormat = GetImageFormat(newpath);

				SysFile fileData = new SysFile()
				{
					Name = fileChunk.FileName,
					Path = Path.Combine(_env.WebRootPath, "Upload", "Files", fileChunk.FileName)
					 .Replace(_env.WebRootPath, "")
					 .Replace(Path.DirectorySeparatorChar, '/'),
					Type = imageFormat.ToString()
				};

				var repositoryresult = await _sysfileRepo.Create(fileData);
				responseData.Success = true;
				responseData.Data = new
				{
					FilePath = Path.Combine(_env.WebRootPath, newpath.Replace(Path.DirectorySeparatorChar, '/'))
				};
			}
			catch (Exception ex)
			{
				responseData.Success = false;
				responseData.Message = ex.Message;
			}

			return responseData;
		}

		public async Task<ResponseResult> GetAllByType(string fileType, int pageSize, int pageNumber)
		{
			ResponseResult result = new ResponseResult();
			Pagination data = await _sysfileRepo.GetAllPagination(pageNumber, pageSize, x => x.Type == fileType, x => x.Id);
			foreach (var entity in data.Records)
			{
				GetAllEntry(entity);
			}
			foreach (var record in data.Records)
			{
				record.Path = CombineUrlWithDomain(record.Path);
			}
			data.Records = data.Records.Select(r => _mapper.Map<SysFile, SysFileGetAllVModel>(r));
			result.Data = data;
			result.Success = true;
			return result;
		}

		private string CombineUrlWithDomain(string path)
		{
			string domain = GetBaseUrl();
			return new Uri(new Uri(domain), path).ToString();
		}
		public virtual void GetAllEntry(SysFile entity)
		{
			//_repository.EntryReference(entity, x => x.Id);
			// override this function in child class if needed
		}
	}
}
