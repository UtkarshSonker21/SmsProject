using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.Label;
using ScholarshipManagement.DTOs.SuperAdmin.Labels;
using ScholarshipManagement.Helper.Enums;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class LabelService : BaseService
    {
        public LabelService(IHttpClientFactory factory) : base(factory) { }

        public async Task<(PagedResultDto<LabelRequestDto> Data, ApiResponseDto Response)> GetLabels(LabelFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/labels/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<LabelRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }

        public async Task<(LabelRequestDto? Data, ApiResponseDto Response)> GetLabelById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/labels/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<LabelRequestDto>(apiResponse);

            return (data, apiResponse);
        }

        public async Task<ApiResponseDto> AddLabel(LabelRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/labels/create", dto);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> UpdateLabel(LabelRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/labels/update/{dto.LableId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteLabel(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/labels/delete/{id}");

            return await HandleResponse(response);
        }




        public async Task<(LanguageLabelsDto Data, ApiResponseDto Response)> GetTranslations(LanguageCode language)
        {
            var response = await _http.GetAsync(
                $"superadmin/labels/translations/{language.ToString().ToLower()}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<LanguageLabelsDto>(apiResponse) ?? new LanguageLabelsDto();

            return (data, apiResponse);
        }



    }
}
