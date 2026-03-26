using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.University.MasterCourseType
{
    public class CourseTypeFilterDto : BaseFilterDto
    {
        public long? UniversityId { get; set; }
        public int? ApprovalStatus { get; set; }
        public bool? IsActive { get; set; }

    }
}
