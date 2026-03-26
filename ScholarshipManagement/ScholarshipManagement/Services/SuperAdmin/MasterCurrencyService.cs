using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.MasterCurrency;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class MasterCurrencyService :BaseService
    {
        public MasterCurrencyService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<MasterCurrencyRequestDto> Data, ApiResponseDto Response)> GetMasterCurrency(MasterCurrencyFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-currency/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<MasterCurrencyRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(MasterCurrencyRequestDto? Data, ApiResponseDto)> GetMasterCurrencyById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/master-currency/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<MasterCurrencyRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddMasterCurrency(MasterCurrencyRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/master-currency/create", dto);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> UpdateMasterCurrency(MasterCurrencyRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/master-currency/update/{dto.CurrencyId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterCurrency(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/master-currency/delete/{id}");

            return await HandleResponse(response);
        }




    }
}
