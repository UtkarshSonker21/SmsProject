using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.MasterCountry
{
    public class MasterCountryFilterDto :BaseFilterDto
    {
        public string? CountryName { get; set; }

        public int? CountryIsdCode { get; set; }

        public string? CountryAlphaCode3 { get; set; }

        public string? CurrencyName { get; set; }

        public bool? IsActive { get; set; }
    }
}
