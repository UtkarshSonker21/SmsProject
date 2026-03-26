using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.Ngo;
using ScholarshipManagement.DTOs.School.MasterSchool;
using ScholarshipManagement.DTOs.University.CourseRequirement;
using ScholarshipManagement.DTOs.University.MasterCourse;
using ScholarshipManagement.DTOs.University.MasterCourseType;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.Ngo
{
    public class ApprovalService : BaseService
    {
        public ApprovalService(IHttpClientFactory factory) : base(factory) { }

        public async Task<ApiResponseDto> ApproveCourseType(ApprovalRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("ngo/approval/course-type", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> ApproveCourse(ApprovalRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("ngo/approval/course", dto);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> ApproveCourseRequirement(ApprovalRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("ngo/approval/course-req", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> ApproveSchool(ApprovalRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync("ngo/approval/school", dto);

            return await HandleResponse(response);
        }



    }
}
