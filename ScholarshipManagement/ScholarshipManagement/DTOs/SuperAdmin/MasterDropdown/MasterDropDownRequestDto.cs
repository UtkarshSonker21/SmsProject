using System.ComponentModel.DataAnnotations;

namespace ScholarshipManagement.DTOs.SuperAdmin.MasterDropdown
{
    public class MasterDropDownRequestDto
    {
        public long? UniqueId { get; set; }   // null or 0 = create

        [Required(ErrorMessage = "DisplayText is required")]
        [StringLength(500)]
        public string DisplayText { get; set; } = null!;

        public long? ParentId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Display Sequence must be greater than 0")]
        public int? DisplaySequence { get; set; }
        public int? NoSeats { get; set; }

        public bool Status { get; set; }

        public bool IsEditable { get; set; }

        public bool IsShow { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } // dd-mm-yyyy hh:mm:ss

        public long? ModuleId { get; set; }


        // response
        public string? ModuleName { get; set; }
    }

}
