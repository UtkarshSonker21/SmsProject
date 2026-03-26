using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.University.MasterCourseType;
using ScholarshipManagement.DTOs.University.MasterUniversity;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.University
{
    public class CourseTypeService : BaseService
    {
        public CourseTypeService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<CourseTypeRequestDto> Data, ApiResponseDto Response)> GetCourseType(CourseTypeFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course-type/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<CourseTypeRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(CourseTypeRequestDto? Data, ApiResponseDto Response)> GetCourseTypeById(long id)
        {
            var response = await _http.GetAsync(
                $"university/master-course-type/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<CourseTypeRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddCourseType(CourseTypeRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course-type/create", dto);

            return await HandleResponse(response);
        }



        public async Task<ApiResponseDto> UpdateCourseType(CourseTypeRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"university/master-course-type/update/{dto.CourseTypeId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteCourseType(long id)
        {
            var response = await _http.DeleteAsync(
                $"university/master-course-type/delete/{id}");

            return await HandleResponse(response);
        }




    }
}
