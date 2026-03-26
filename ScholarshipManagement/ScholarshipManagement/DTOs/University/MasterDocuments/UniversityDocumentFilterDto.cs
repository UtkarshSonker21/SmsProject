using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.University.MasterDocuments
{
    public class UniversityDocumentFilterDto : BaseFilterDto
    {
        public long? UniversityId { get; set; }

        public bool? IsActive { get; set; }

    }
}
