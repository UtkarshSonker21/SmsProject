using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.School.Students;
using ScholarshipManagement.DTOs.University.CourseRequirement;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.University
{
    public class CourseRequirementService:BaseService
    {
        public CourseRequirementService(IHttpClientFactory factory) : base(factory) { }

        public async Task<(PagedResultDto<CourseRequirementRequestDto> Data, ApiResponseDto Response)> GetCourseRequirement(CourseRequirementFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course-requirement/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<CourseRequirementRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(CourseRequirementRequestDto? Data, ApiResponseDto Response)> GetCourseRequirementById(long id)
        {
            var response = await _http.GetAsync(
                $"university/master-course-requirement/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<CourseRequirementRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddCourseRequirement(CourseRequirementRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course-requirement/create", dto);

            return await HandleResponse(response);
        }



        public async Task<ApiResponseDto> UpdateCourseRequirement(CourseRequirementRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"university/master-course-requirement/update/{dto.ReqId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteCourseRequirement(long id)
        {
            var response = await _http.DeleteAsync(
                $"university/master-course-requirement/delete/{id}");

            return await HandleResponse(response);
        }


        public async Task<(PagedResultDto<CourseRequirementEnrollmentDto> Data, ApiResponseDto Response)> GetEnrollmentsAsync(CourseRequirementFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "university/master-course-requirement/enrollments/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<CourseRequirementEnrollmentDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(PagedResultDto<EnrolledStudentDto> Data, ApiResponseDto Response)> GetEnrolledStudents(long reqId, StudentFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                $"university/master-course-requirement/enrollments/students/search/{reqId}", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<EnrolledStudentDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


    }
}
