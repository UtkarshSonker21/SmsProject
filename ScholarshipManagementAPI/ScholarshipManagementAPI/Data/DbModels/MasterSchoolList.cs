using System;
using System.Collections.Generic;

namespace ScholarshipManagementAPI.Data.DbModels;

public partial class MasterSchoolList
{
    public long SchoolId { get; set; }

    public string SchoolName { get; set; } = null!;

    public string? StudentCodeFormatPrefix { get; set; }

    public string? StudentCodeFormatSufix { get; set; }

    public string? StudentCodeFormatStartingNo { get; set; }

    public string? StudentCodeFormatLastSavedNumber { get; set; }

    public DateTime CreatedDate { get; set; }

    public string ShortName { get; set; } = null!;

    public long CountryId { get; set; }

    public string? Area { get; set; }

    public string? CenterName { get; set; }

    public string? SchoolNumber { get; set; }

    public string? SchoolYearOfEstablish { get; set; }

    public string? SchoolType { get; set; }

    public string? SchoolTeachingLanguage { get; set; }

    public string? GraduatesEnglishLessThan { get; set; }

    public string? TotalNumberOfHighSchoolLevel { get; set; }

    public string? AverageNumberOfStudentPerClass { get; set; }

    public string? SchoolAccreditations { get; set; }

    public string? SchoolSubjectCurriculum { get; set; }

    public string? StudentsuccessAverage { get; set; }

    public string? AverageSchoolGraduates { get; set; }

    public string? SchoolLocalRank { get; set; }

    public string? OwningInstitution { get; set; }

    public string? SchoolWebsite { get; set; }

    public string? SchoolPhoneNo { get; set; }

    public bool IsActive { get; set; }

    public int? SchoolStatus { get; set; }

    public string? EmailId { get; set; }

    public string? SchoolCoOrdenatorName { get; set; }

    public string? SchoolCoOrdenatorMobile { get; set; }

    public string? SchoolCoOrdenatorEmail { get; set; }

    public DateTime? AcademicyearStartDate { get; set; }

    public DateTime? AcademicyearEndDate { get; set; }

    public int ApprovalStatus { get; set; }

    public long? ApprovedBy { get; set; }

    public int StudentSequenceNumber { get; set; }

    public long? DefaultCurrencyId { get; set; }

    public virtual UsersLogin? ApprovedByNavigation { get; set; }

    public virtual ZzMasterCountry Country { get; set; } = null!;

    public virtual ZzMasterCurrency? DefaultCurrency { get; set; }

    public virtual ICollection<HrStaffMaster> HrStaffMasters { get; set; } = new List<HrStaffMaster>();

    public virtual ICollection<StudentDatum> StudentData { get; set; } = new List<StudentDatum>();
}
