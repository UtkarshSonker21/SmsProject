using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.MasterDropdown
{
    public class MasterDropDownFilterDto : BaseFilterDto
    {
        public long? ModuleId { get; set; }
        public long? ParentId { get; set; }
        public bool? Status { get; set; }
        public bool? IsShow { get; set; }
        //public string? SearchText { get; set; }
    }
}
