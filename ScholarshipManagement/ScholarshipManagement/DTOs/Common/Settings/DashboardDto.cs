namespace ScholarshipManagement.DTOs.Common.Settings
{
    public class DashboardDto
    {
        // Fixed (core business metrics)
        public int TotalStudents { get; set; }
        public int TotalApplications { get; set; }

        // public int NominatedCandidates { get; set; }
        public int AcceptedApplications { get; set; }
        public int SponsoredCandidates { get; set; }


        // Growth metrics
        public int NewStudentsThisMonth { get; set; }
        public int ApplicationsThisMonth { get; set; }

        public decimal StudentsGrowthPercentage { get; set; }
        public decimal ApplicationsGrowthPercentage { get; set; }



        // NEW: Dynamic cards (future-proof)
        public List<DashboardCardDto> Cards { get; set; } = new();


    }
}
