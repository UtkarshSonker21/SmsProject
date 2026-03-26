using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersLoginLog
{
    public class UsersLoginLogFilterDto : BaseFilterDto
    {
        public long? LoginId { get; set; }

        public DateTime? LoginFrom { get; set; }
        public DateTime? LoginTo { get; set; }
    }
}
