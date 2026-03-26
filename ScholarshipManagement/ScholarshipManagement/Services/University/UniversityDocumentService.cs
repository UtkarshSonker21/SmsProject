using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.University.MasterDocuments;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.University
{
    public class UniversityDocumentService : BaseService
    {
        public UniversityDocumentService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<UniversityDocumentRequestDto> Data, ApiResponseDto Response)> GetMasterUniversityDocuments(UniversityDocumentFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-document/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UniversityDocumentRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UniversityDocumentRequestDto? Data, ApiResponseDto Response)> GetMasterUniversityDocumentById(long id)
        {
            var response = await _http.GetAsync(
                $"university/master-document/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UniversityDocumentRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddMasterUniversityDocument(UniversityDocumentRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-document/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateMasterUniversityDocument(UniversityDocumentRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"university/master-document/update/{dto.UniversityId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteMasterUniversityDocument(long id)
        {
            var response = await _http.DeleteAsync(
                $"university/master-document/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
