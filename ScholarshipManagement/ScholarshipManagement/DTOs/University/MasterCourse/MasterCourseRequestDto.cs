using System.ComponentModel.DataAnnotations;

namespace ScholarshipManagement.DTOs.University.MasterCourse
{
    public class MasterCourseRequestDto
    {
        public long? CourseId { get; set; } // null for create, value for update

        [Required]
        public long UniversityId { get; set; }

        [Required]
        [StringLength(200)]
        public string CourseName { get; set; } = string.Empty;

        [StringLength(200)]
        public string? CourseCode { get; set; }

        [Required]
        public long CourseTypeId { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        public long? Duration { get; set; }

        [Required]
        [StringLength(200)]
        public string DurationUnit { get; set; } = string.Empty; // Years / Months

        [Required(ErrorMessage = "Semester is required")]
        public int? NoSemester { get; set; }


        [StringLength(500)]
        public string? Remarks { get; set; }

        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; } = string.Empty;

        public int ApprovalStatus { get; set; }
        public long? ApprovedBy { get; set; }

        [Required]
        public bool IsActive { get; set; }


        // for Response
        public string? CourseTypeName { get; set; }
        public string? UniversityName { get; set; }
        public string? ApprovedByName { get; set; }
    }
}
