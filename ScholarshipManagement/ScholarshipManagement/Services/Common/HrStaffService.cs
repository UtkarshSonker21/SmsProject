using ScholarshipManagement.DTOs.Common.HrStaff;
using ScholarshipManagement.DTOs.Common.Response;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.Common
{
    public class HrStaffService : BaseService
    {
        public HrStaffService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<StaffRequestDto> Data, ApiResponseDto Response)> GetHrStaffs(StaffFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "common/staff/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<StaffRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(StaffRequestDto Data, ApiResponseDto Response)> GetHrStaffById(long id)
        {
            var response = await _http.GetAsync(
                $"common/staff/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<StaffRequestDto>(apiResponse) ?? new StaffRequestDto();

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddHrStaff(StaffRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "common/staff/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateHrStaff(StaffRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"common/staff/update/{dto.StaffId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteHrStaff(long id)
        {
            var response = await _http.DeleteAsync(
                $"common/staff/delete/{id}");

            return await HandleResponse(response);
        }

    }
}
