using Microsoft.AspNetCore.Components.Forms;
using ScholarshipManagement.DTOs.Common.Menu;
using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.School.StudentRequirements;
using ScholarshipManagement.DTOs.School.Students;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using static ScholarshipManagement.Helper.Constant;
using static System.Net.WebRequestMethods;

namespace ScholarshipManagement.Services.School
{
    public class StudentRequirementService : BaseService
    {
        //private readonly HttpClient _http;

        public StudentRequirementService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<StudentRequirementRequestDto> Data, ApiResponseDto Response)> GetStudentRequirements(StudentRequirementFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "school/student-req/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<StudentRequirementRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



        public async Task<(StudentRequirementRequestDto? Data, ApiResponseDto Response)> GetStudentRequirementById(long id)
        {
            var response = await _http.GetAsync(
                $"school/student-req/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<StudentRequirementRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> CreateStudentRequirementMapAsync(StudentRequirementMappingDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "school/student-req/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateStudentRequirementMapAsync(StudentRequirementMappingDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"school/student-req/update/{dto.StudentReqID}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateStudentRequirementMapByUniversityAsync(StudentRequirementRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"school/student-req/update-by-university/{dto.StudentReqID}", dto);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> UpdateStudentRequirementMapByNgoAsync(StudentRequirementRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"school/student-req/update-by-ngo/{dto.StudentReqID}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteStudentRequirement(long id)
        {
            var response = await _http.DeleteAsync(
                $"school/student-req/delete/{id}");

            return await HandleResponse(response);
        }



        // 🔹 GET DOCUMENTS
        public async Task<(List<StudentDocumentDto> Data, ApiResponseDto Response)> GetDocumentsAsync(long studentReqId)
        {
            var response = await _http.GetAsync($"school/student-req/documents/{studentReqId}");

            var apiResponse = await HandleResponse(response);

            var data = GetList<StudentDocumentDto>(apiResponse) ?? new List<StudentDocumentDto>();

            return (data, apiResponse);
        }



        // 🔹 UPLOAD DOCUMENT
        public async Task<ApiResponseDto> UploadDocumentAsync(long studentReqId, long masterDocId, IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();

            var fileContent = new StreamContent(file.OpenReadStream(5 * 1024 * 1024));
            fileContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            content.Add(fileContent, "file", file.Name);
            content.Add(new StringContent(studentReqId.ToString()), "studentReqId");
            content.Add(new StringContent(masterDocId.ToString()), "masterDocId");

            var response = await _http.PostAsync($"school/student-req/upload/{studentReqId}/{masterDocId}", content);

            return await HandleResponse(response);
        }


        public async Task<(List<StudentDocumentDto> Data, ApiResponseDto Response)> GetDocumentsAsync(
            long? studentReqId,
            Guid? sessionId,
            long reqId)
        {
            var request = new DocumentStatusRequestDto
            {
                StudentReqId = studentReqId,
                UploadSessionId = sessionId,
                ReqId = reqId
            };

            var response = await _http.PostAsJsonAsync("school/student-req/document-status", request);

            var apiResponse = await HandleResponse(response);

            var data = GetList<StudentDocumentDto>(apiResponse) ?? new List<StudentDocumentDto>();

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> UploadDocumentAsync(
            Guid sessionId,
            long? studentReqId,
            long studentId,
            long masterDocId,
            IBrowserFile file)
        {
            using var content = new MultipartFormDataContent();

            var streamContent = new StreamContent(file.OpenReadStream(5 * 1024 * 1024));
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);

            // 🔹 File
            content.Add(streamContent, "File", file.Name);

            // 🔹 DTO fields (IMPORTANT: names must match DTO)
            content.Add(new StringContent(sessionId.ToString()), "UploadSessionId");
            content.Add(new StringContent(studentId.ToString()), "StudentId");
            content.Add(new StringContent(masterDocId.ToString()), "MasterDocId");

            if (studentReqId != null)
                content.Add(new StringContent(studentReqId.ToString()), "StudentReqId");

            var response = await _http.PostAsync("school/student-req/upload", content);

            return await HandleResponse(response);
        }


    }
}
