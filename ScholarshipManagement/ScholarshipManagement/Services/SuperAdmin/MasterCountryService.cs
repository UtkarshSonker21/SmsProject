using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.Ngo.CountrySchoolsSummary;
using ScholarshipManagement.DTOs.SuperAdmin.MasterCountry;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class MasterCountryService : BaseService
    {
        public MasterCountryService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<MasterCountryRequestDto> Data, ApiResponseDto Response)> GetMasterCountry(MasterCountryFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-country/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<MasterCountryRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(MasterCountryRequestDto? Data, ApiResponseDto)> GetMasterCountryById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/master-country/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<MasterCountryRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddMasterCountry(MasterCountryRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-country/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateMasterCountry(MasterCountryRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/master-country/update/{dto.CountryId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterCountry(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/master-country/delete/{id}");

            return await HandleResponse(response);
        }


        public async Task<(PagedResultDto<CountrySchoolCountDto> Data, ApiResponseDto Response)> GetCountryWiseSchoolCountAsync(MasterCountryFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-country/country-schools", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<CountrySchoolCountDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



    }
}
