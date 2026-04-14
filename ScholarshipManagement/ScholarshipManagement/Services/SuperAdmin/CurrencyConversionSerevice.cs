
using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.CurrencyConversion;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class CurrencyConversionSerevice : BaseService
    {
        public CurrencyConversionSerevice(IHttpClientFactory factory) : base(factory) { }

        public async Task<(PagedResultDto<CurrencyConversionRequestDto> Data, ApiResponseDto Response)> GetCurrencyConversion(CurrencyConversionFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/currency-conversion/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<CurrencyConversionRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(CurrencyConversionRequestDto? Data, ApiResponseDto)> GetCurrencyConversionById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/currency-conversion/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<CurrencyConversionRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<(List<CurrencyConversionRequestDto> Data, ApiResponseDto Response)> GetCurrentCurrencyRate()
        {
            var response = await _http.GetAsync("superadmin/currency-conversion/latest-rates");

            var apiResponse = await HandleResponse(response);

            var data = GetList<CurrencyConversionRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddCurrencyConversion(CurrencyConversionRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/currency-conversion/create", dto);

            return await HandleResponse(response);
        }


    }
}
