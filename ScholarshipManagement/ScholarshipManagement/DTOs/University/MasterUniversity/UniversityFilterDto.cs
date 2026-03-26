using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.University.MasterUniversity
{
    public class UniversityFilterDto:BaseFilterDto
    {
        public long? CountryId { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsApproved { get; set; }

        public long? ApprovedBy { get; set; }

        public DateTime? CreatedFrom { get; set; }

        public DateTime? CreatedTo { get; set; }

    }
}
