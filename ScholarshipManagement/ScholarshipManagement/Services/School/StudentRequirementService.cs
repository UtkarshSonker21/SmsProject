using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.School.StudentRequirements;
using ScholarshipManagement.DTOs.School.Students;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;
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


        public async Task<ApiResponseDto> DeleteStudentRequirement(long id)
        {
            var response = await _http.DeleteAsync(
                $"school/student-req/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
