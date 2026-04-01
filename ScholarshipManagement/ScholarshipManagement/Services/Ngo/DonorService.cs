using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.Ngo.Donor;
using ScholarshipManagement.DTOs.School.MasterSchool;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.Ngo
{
    public class DonorService : BaseService
    {
        //private readonly HttpClient _http;

        public DonorService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<DonorRequestDto> Data, ApiResponseDto Response)> GetDonor(DonorFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "ngo/donor/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<DonorRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(DonorRequestDto? Data, ApiResponseDto Response)> GetDonorById(long id)
        {
            var response = await _http.GetAsync(
                $"ngo/donor/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<DonorRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddDonor(DonorRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "ngo/donor/create", dto);

            return await HandleResponse(response);
        }




        public async Task<ApiResponseDto> UpdateDonor(DonorRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"ngo/donor/update/{dto.DonorId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteDonor(long id)
        {
            var response = await _http.DeleteAsync(
                $"ngo/donor/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
