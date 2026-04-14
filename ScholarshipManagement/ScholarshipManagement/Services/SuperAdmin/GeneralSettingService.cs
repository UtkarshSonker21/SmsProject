using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.GeneralSettings;
using ScholarshipManagement.Services.Common;
using ScholarshipManagementAPI.DTOs.Common.Settings;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class GeneralSettingService : BaseService
    {
        public GeneralSettingService(IHttpClientFactory factory) : base(factory) { }

        public async Task<(PagedResultDto<GeneralSettingRequestDto> Data, ApiResponseDto Response)> GetGeneralSettings(GeneralSettingFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/general-settings/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<GeneralSettingRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(GeneralSettingRequestDto? Data, ApiResponseDto)> GetGeneralSettingsById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/general-settings/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<GeneralSettingRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddGeneralSettings(GeneralSettingRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/general-settings/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateGeneralSettings(GeneralSettingRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/general-settings/update/{dto.ConfigId}", dto);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> DeleteGeneralSettings(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/general-settings/delete/{id}");

            return await HandleResponse(response);
        }


        public async Task<(BaseCurrencyDto Data, ApiResponseDto)> GetBaseCurrency()
        {
            var response = await _http.GetAsync(
                "superadmin/general-settings/base-currency");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<BaseCurrencyDto>(apiResponse) ?? new BaseCurrencyDto();

            return (data, apiResponse);
        }



    }
}
