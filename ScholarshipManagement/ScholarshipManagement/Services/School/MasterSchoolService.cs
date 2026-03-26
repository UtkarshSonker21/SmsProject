using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.School.MasterSchool;
using ScholarshipManagement.DTOs.SuperAdmin.UsersMenu;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.School
{
    public class MasterSchoolService : BaseService
    {
        //private readonly HttpClient _http;

        public MasterSchoolService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<MasterSchoolRequestDto> Data, ApiResponseDto Response)> GetMasterSchool(MasterSchoolFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "school/master-school/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<MasterSchoolRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



        public async Task<(MasterSchoolRequestDto? Data, ApiResponseDto Response)> GetMasterSchoolById(long id)
        {
            var response = await _http.GetAsync(
                $"school/master-school/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<MasterSchoolRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddMasterSchool(MasterSchoolRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "school/master-school/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateMasterSchool(MasterSchoolRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"school/master-school/update/{dto.SchoolId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterSchool(long id)
        {
            var response = await _http.DeleteAsync(
                $"school/master-school/delete/{id}");

            return await HandleResponse(response);
        }

    }
}
