using ScholarshipManagementAPI.DTOs.Common.Auth;
using ScholarshipManagementAPI.DTOs.Common.Response;
using ScholarshipManagementAPI.DTOs.School.Students;
using ScholarshipManagementAPI.DTOs.University.CourseRequirement;

namespace ScholarshipManagementAPI.Services.Interface.University
{
    public interface ICourseRequirementService
    {
        Task<long> CreateAsync(CourseRequirementRequestDto dto);
        Task<bool> UpdateAsync(CourseRequirementRequestDto dto);
        Task<bool> DeleteAsync(long id);

        Task<CourseRequirementRequestDto?> GetByIdAsync(long id);
        Task<PagedResultDto<CourseRequirementRequestDto>> GetByFilterAsync(CourseRequirementFilterDto filter, LoggedInUserDto currentUser);



        Task<PagedResultDto<CourseRequirementEnrollmentDto>> GetEnrollmentsAsync(CourseRequirementFilterDto filter, LoggedInUserDto currentUser);

        Task<PagedResultDto<EnrolledStudentDto>> GetEnrolledStudentsAsync(long reqId, StudentFilterDto filter);

    }
}
