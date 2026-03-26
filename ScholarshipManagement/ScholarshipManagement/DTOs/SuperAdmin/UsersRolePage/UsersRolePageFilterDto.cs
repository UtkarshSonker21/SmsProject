using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersRolePage
{
    public class UsersRolePageFilterDto:BaseFilterDto
    {
        /* -------- RolePage specific -------- */
        public long? RoleId { get; set; }
        public long? MenuLinkId { get; set; }



        /* -------- Role based filters -------- */
        public long? ModuleId { get; set; }
        public bool? IsActive { get; set; }
        public long? DashboardMenuLinkId { get; set; }
    }
}
