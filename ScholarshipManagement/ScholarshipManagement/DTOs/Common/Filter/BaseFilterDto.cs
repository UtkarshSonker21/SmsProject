namespace ScholarshipManagement.DTOs.Common.Filter
{
    public class BaseFilterDto
    {
        public int PageNumber { get; set; } = 1;   // default first page
        public int PageSize { get; set; } = 25;    // default page size

        public string? SearchText { get; set; }     // global search text
    }
}
