using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.GeneralSettings
{
    public class GeneralSettingFilterDto:BaseFilterDto
    {
        public string? ConfigKey { get; set; }
        public string? ConfigValue { get; set; }
    }
}
