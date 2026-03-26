using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.Common.HrStaff
{
    public class StaffFilterDto : BaseFilterDto
    {
        public long? StaffType { get; set; }        // Filter by module
        public long? OrganisationId { get; set; }   // School / University / Ngo
        public bool? IsActive { get; set; }
    }
}
