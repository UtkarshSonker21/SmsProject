using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersRole
{
    public class UsersRoleFilterDto : BaseFilterDto
    {
        public long? ModuleId { get; set; }
        public long? DashboardMenuLinkId { get; set; }
        public bool? IsActive { get; set; }
    }
}
