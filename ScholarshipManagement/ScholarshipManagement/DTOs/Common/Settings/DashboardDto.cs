namespace ScholarshipManagement.DTOs.Common.Settings
{
    public class DashboardDto
    {
        public int TotalStudents { get; set; }
        public int TotalApplications { get; set; }

        public int PendingApplications { get; set; }
        public int ApprovedApplications { get; set; }
        public int RejectedApplications { get; set; }






        // Optional enhancements for growth metrics
        public int NewStudentsThisMonth { get; set; }
        public int ApplicationsThisMonth { get; set; }

        public decimal StudentsGrowthPercentage { get; set; }
        public decimal ApplicationsGrowthPercentage { get; set; }
    }
}
