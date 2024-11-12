using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BlogManagement.Domain.Constants
{
	public static class Utilities
	{
		public static string MakeExceptionMessage(Exception ex)
		{
			return ex.InnerException == null ? ex.Message : ex.InnerException.Message;
		}

		public static long GetValueOfTotalRecords(List<dynamic> recordsTotal)
		{
			if (recordsTotal != null && recordsTotal.Count == 1)
			{
				return recordsTotal[0].TotalRecords;
			}
			else return 0;
		}

		public static string RandomString(int length)
		{
			var random = new Random();
			const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
			return new string(Enumerable.Repeat(chars, length)
				.Select(s => s[random.Next(s.Length)]).ToArray());
		}

		public static T ConvertModel<T>(object entity)
		{
			string origin = JsonConvert.SerializeObject(entity);
			return JsonConvert.DeserializeObject<T>(origin);
		}

		public static T ConvertModel<T>(string entity)
		{
			return JsonConvert.DeserializeObject<T>(entity);
		}

		//public static List<RoleVModel> ConvertModel(this List<string> entitys)
		//{
		//	List<RoleVModel> returnRoleVModel = new List<RoleVModel>();
		//	if (entitys.Count() == 1)
		//	{
		//		if (string.IsNullOrWhiteSpace(entitys[0]))
		//			return null;

		//		return JsonConvert.DeserializeObject<List<RoleVModel>>(entitys[0]);
		//	}

		//	int first = 0;
		//	foreach (string entity in entitys)
		//	{
		//		if (first == 0)
		//		{
		//			returnRoleVModel = JsonConvert.DeserializeObject<List<RoleVModel>>(entity);
		//			first++;
		//			continue;
		//		}

		//		List<RoleVModel> roleVModel = JsonConvert.DeserializeObject<List<RoleVModel>>(entity);

		//		for (int i = 0; i < returnRoleVModel.Count; i++)
		//		{
		//			for (int j = 0; j < returnRoleVModel[i].PermissionModels.Count; j++)
		//			{
		//				if (returnRoleVModel[i].PermissionModels[j].IsAllow == false && roleVModel[i].PermissionModels[j].IsAllow)
		//				{
		//					returnRoleVModel[i].PermissionModels[j].IsAllow = true;
		//				}
		//			}
		//		}
		//	}

		//	return returnRoleVModel;
		//}

		//public static string ConvertModel(this string role, string newRole)
		//{
		//	List<RoleVModel> roleVModel = JsonConvert.DeserializeObject<List<RoleVModel>>(role);
		//	List<RoleVModel> newRoleVModel = JsonConvert.DeserializeObject<List<RoleVModel>>(newRole);

		//	for (int i = 0; i < newRoleVModel.Count; i++)
		//	{
		//		if (i == roleVModel.Count)
		//			break;

		//		for (int j = 0; j < newRoleVModel[i].PermissionModels.Count; j++)
		//		{
		//			if (j == roleVModel[i].PermissionModels.Count)
		//				break;

		//			if (newRoleVModel[i].PermissionModels[j].IsAllow == false && roleVModel[i].PermissionModels[j].IsAllow)
		//			{
		//				newRoleVModel[i].PermissionModels[j].IsAllow = true;
		//			}
		//		}
		//	}

		//	return JsonConvert.SerializeObject(newRoleVModel);
		//}

		public struct SlugGenerator
		{
			public static string GenerateAliasFromName(string name)
			{
				if (string.IsNullOrWhiteSpace(name))
				{
					return string.Empty;
				}
				string slug = name.ToLower();
				slug = slug.Replace("đ", "d");
				slug = RemoveAccents(slug);
				slug = Regex.Replace(slug, "[^a-zA-Z0-9- ]", "");
				slug = slug.Replace(" ", "-");
				slug = System.Text.RegularExpressions.Regex.Replace(slug, @"-+", "-");
				slug = slug.Trim('-');
				return slug;
			}
			private static string RemoveAccents(string input)
			{
				string normalizedString = input.Normalize(NormalizationForm.FormKD);
				return new string(normalizedString
					.Where(c => CharUnicodeInfo.GetUnicodeCategory(c) != UnicodeCategory.NonSpacingMark)
					.ToArray());
			}

		}
		//public static async Task<bool> CheckLanguage(long id, IBaseRepository<SysLanguage> syslanguageRepo)
		//{
		//	var checkLanguage = await syslanguageRepo.GetById(id);
		//	if (checkLanguage == null)
		//	{
		//		return false;
		//	}
		//	if (checkLanguage.Code == CommonConstants.Language.Vietnamese || checkLanguage.Code == CommonConstants.Language.VietNam || checkLanguage.Code == CommonConstants.Language.English || checkLanguage.Code == CommonConstants.Language.TiengAnh)
		//	{
		//		return true;
		//	}
		//	return false;
		//}
		//public static async Task<ResponseResult> CheckDatesAndCreate<T>(BaseEntity entity, Func<Task<ResponseResult>> createFunction) where T : BaseEntity
		//{
		//    var result = new ResponseResult();
		//    try
		//    {
		//        DateTime dateTime = DateTime.Now;
		//        if (entity.DateOfBirth <= dateTime)
		//        {
		//            if (entity.StartDate >= dateTime)
		//            {
		//                result = await createFunction.Invoke();
		//            }
		//            else
		//            {
		//                result.Message = "The start date must be greater than the current date.";
		//                result.Success = false;
		//            }
		//        }
		//        else
		//        {
		//            result.Message = "Date Of Birth does not exist.";
		//            result.Success = false;
		//        }
		//    }
		//    catch (Exception ex)
		//    {
		//        result.Message = MakeExceptionMessage(ex);
		//        result.Success = false;
		//    }
		//    return result;
		//}
	}
}
