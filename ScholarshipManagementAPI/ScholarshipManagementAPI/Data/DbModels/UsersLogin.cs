using System;
using System.Collections.Generic;

namespace ScholarshipManagementAPI.Data.DbModels;

public partial class UsersLogin
{
    public long LoginId { get; set; }

    public long StaffId { get; set; }

    public string LoginName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ForgotEmail { get; set; } = null!;

    public bool IsActive { get; set; }

    public DateTime CreatedDate { get; set; }

    public string CreatedBy { get; set; } = null!;

    public string? Language { get; set; }

    public string? TempPassword { get; set; }

    public DateTime? TempPassDateTime { get; set; }

    public virtual ICollection<MasterSchoolList> MasterSchoolLists { get; set; } = new List<MasterSchoolList>();

    public virtual HrStaffMaster Staff { get; set; } = null!;

    public virtual ICollection<UnCourseReq> UnCourseReqs { get; set; } = new List<UnCourseReq>();

    public virtual ICollection<UnMasterCourseType> UnMasterCourseTypes { get; set; } = new List<UnMasterCourseType>();

    public virtual ICollection<UnMasterCourse> UnMasterCourses { get; set; } = new List<UnMasterCourse>();

    public virtual ICollection<UsersLoginRole> UsersLoginRoles { get; set; } = new List<UsersLoginRole>();

    public virtual ICollection<UsersLoginsLog> UsersLoginsLogs { get; set; } = new List<UsersLoginsLog>();
}
