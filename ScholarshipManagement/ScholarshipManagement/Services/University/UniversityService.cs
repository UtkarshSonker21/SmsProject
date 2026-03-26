using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.MasterCountry;
using ScholarshipManagement.DTOs.University.MasterUniversity;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.University
{
    public class UniversityService :BaseService
    {
        public UniversityService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<UniversityRequestDto> Data, ApiResponseDto Response)> GetMasterUniversity(UniversityFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-university/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UniversityRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UniversityRequestDto? Data, ApiResponseDto Response)> GetMasterUniversityById(long id)
        {
            var response = await _http.GetAsync(
                $"university/master-university/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UniversityRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddMasterUniversity(UniversityRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-university/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateMasterUniversity(UniversityRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"university/master-university/update/{dto.UniversityId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterUniversity(long id)
        {
            var response = await _http.DeleteAsync(
                $"university/master-university/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
