using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersMenu
{
    public class UsersMenuFilterDto :BaseFilterDto
    {
        public long? ModuleId { get; set; }
        public long? ParentId { get; set; }
        public bool? IsView { get; set; }
        public bool? ShowInMenu { get; set; }
        public bool? IsDashboard { get; set; }
        public bool? IsApp { get; set; }
    }
}
