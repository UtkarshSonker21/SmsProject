using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.University.MasterCourse;
using ScholarshipManagement.DTOs.University.MasterCourseType;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.University
{
    public class CourseService :BaseService
    {
        public CourseService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<MasterCourseRequestDto> Data, ApiResponseDto Response)> GetCourse(MasterCourseFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<MasterCourseRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(MasterCourseRequestDto? Data, ApiResponseDto Response)> GetCourseById(long id)
        {
            var response = await _http.GetAsync(
                $"university/master-course/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<MasterCourseRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddCourse(MasterCourseRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course/create", dto);

            return await HandleResponse(response);
        }



        public async Task<ApiResponseDto> UpdateCourse(MasterCourseRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"university/master-course/update/{dto.CourseId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteCourse(long id)
        {
            var response = await _http.DeleteAsync(
                $"university/master-course/delete/{id}");

            return await HandleResponse(response);
        }



    }
}
