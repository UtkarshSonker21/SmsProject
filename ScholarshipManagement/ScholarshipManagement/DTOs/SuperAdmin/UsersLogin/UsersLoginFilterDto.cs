using ScholarshipManagement.DTOs.Common.Filter;

namespace ScholarshipManagement.DTOs.SuperAdmin.UsersLogin
{
    public class UsersLoginFilterDto : BaseFilterDto
    {
        public bool? IsActive { get; set; }


        //public int? LoginType { get; set; }
        //public long? UniversityId { get; set; }
        //public long? SchoolListId { get; set; }
    }
}
