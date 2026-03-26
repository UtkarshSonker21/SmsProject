using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersLoginRole
{
    public class UsersLoginRoleFilterDto : BaseFilterDto
    {
        public long? RoleId { get; set; }
        public long? LoginId { get; set; }
        public bool? IsDefault { get; set; }
    }
}
