using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.Labels
{
    public class LabelFilterDto :BaseFilterDto
    {
        public string? LabelName { get; set; }

        public string? LabelValue { get; set; }

        public bool? HasNewValue { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }
    }
}
