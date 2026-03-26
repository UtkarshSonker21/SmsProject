using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.MasterDropdown;
using ScholarshipManagement.Services.Common;
using System.Net.Http;
using System.Net.Http.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class MasterDropdownService :BaseService
    {
        //private readonly HttpClient _http;

        public MasterDropdownService(IHttpClientFactory factory) : base(factory) { }




        public async Task<(PagedResultDto<MasterDropDownRequestDto> Data, ApiResponseDto Response)> GetMasterDropDown(MasterDropDownFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-dropdown/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<MasterDropDownRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(MasterDropDownRequestDto? Data, ApiResponseDto Response)> GetMasterDropDownById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/master-dropdown/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<MasterDropDownRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddMasterDropDown(MasterDropDownRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-dropdown/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateMasterDropDown(MasterDropDownRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/master-dropdown/update/{dto.UniqueId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterDropDown(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/master-dropdown/delete/{id}");

            return await HandleResponse(response);
        }


    }

}


//public async Task<ZzMasterDropDownRequestDto> GetMasterDropDown(ZzMasterDropDownFilterDto filter)
//{
//    var response = await _http.PostAsJsonAsync("zz-master-dropdown/filter", filter);

//    if (!response.IsSuccessStatusCode)
//        throw new Exception("API Error: " + response.StatusCode);

//    var result = await response.Content.ReadFromJsonAsync<ZzMasterDropDownRequestDto>();
//    return result ?? new ZzMasterDropDownRequestDto();
//}
