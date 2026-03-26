using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.MasterCurrency
{
    public class MasterCurrencyFilterDto :BaseFilterDto
    {
        public string? CurrencyName { get; set; }

        public string? CurrencyAbb { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }
    }
}
