using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.School.MasterSchool;
using ScholarshipManagement.DTOs.School.Students;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.School
{
    public class StudentService : BaseService
    {
        //private readonly HttpClient _http;

        public StudentService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<StudentRequestDto> Data, ApiResponseDto Response)> GetStudents(StudentFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "school/student/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<StudentRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



        public async Task<(StudentRequestDto Data, ApiResponseDto Response)> GetStudentById(long id)
        {
            var response = await _http.GetAsync(
                $"school/student/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<StudentRequestDto>(apiResponse) ?? new StudentRequestDto();

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddStudent(StudentRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "school/student/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateStudent(StudentRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"school/student/update/{dto.StudentId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteStudent(long id)
        {
            var response = await _http.DeleteAsync(
                $"school/student/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
