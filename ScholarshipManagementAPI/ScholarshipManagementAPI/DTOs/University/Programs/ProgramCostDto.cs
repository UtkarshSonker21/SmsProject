namespace ScholarshipManagementAPI.DTOs.University.Programs
{
    public class ProgramCostDto
    {
        public long? ProgramCostId { get; set; }

        public long SponsorshipTypeId { get; set; }

        public string? SponsorshipTypeName { get; set; }

        public decimal Amount { get; set; }
    }
}
