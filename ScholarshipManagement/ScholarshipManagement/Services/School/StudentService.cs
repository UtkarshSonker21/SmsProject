using Microsoft.AspNetCore.Components.Forms;
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


        public async Task<ApiResponseDto> AddStudent(StudentRequestDto dto, IBrowserFile? file)
        {
            //var response = await _http.PostAsJsonAsync(
            //    "school/student/create", dto);

            var content = ConvertToMultipart(dto, file);

            var response = await _http.PostAsync("school/student/create", content);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateStudent(StudentRequestDto dto, IBrowserFile? file)
        {
            //var response = await _http.PutAsJsonAsync(
            //    $"school/student/update/{dto.StudentId}", dto);

            var content = ConvertToMultipart(dto, file);

            var response = await _http.PutAsync(
                $"school/student/update/{dto.StudentId}", content);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteStudent(long id)
        {
            var response = await _http.DeleteAsync(
                $"school/student/delete/{id}");

            return await HandleResponse(response);
        }




        private MultipartFormDataContent ConvertToMultipart(StudentRequestDto dto, IBrowserFile? file)
        {
            var content = new MultipartFormDataContent();

            var properties = typeof(StudentRequestDto).GetProperties();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(dto);

                if (value == null)
                    continue;

                // Skip response-only fields if needed
                if (prop.Name == "SchoolName" || prop.Name == "ShortName")
                    continue;

                // Convert DateTime properly
                if (value is DateTime dt)
                {
                    content.Add(new StringContent(dt.ToString("o")), prop.Name);
                }
                else
                {
                    content.Add(new StringContent(value.ToString()!), prop.Name);
                }
            }

            // 🔹 Add file separately
            if (file != null)
            {
                var stream = file.OpenReadStream(5 * 1024 * 1024);
                content.Add(new StreamContent(stream), "RecommendationLetterFile", file.Name);
            }

            return content;
        }


    }
}
