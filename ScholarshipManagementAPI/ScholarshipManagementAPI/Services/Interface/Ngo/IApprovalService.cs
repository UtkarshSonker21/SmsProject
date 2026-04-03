namespace ScholarshipManagementAPI.Services.Interface.Ngo
{
    public interface IApprovalService
    {
        Task<bool> ApproveCourseTypeAsync(long id, int approvalStatus, long approvedBy);
        Task<bool> ApproveCourseAsync(long id, int approvalStatus, long approvedBy);
        Task<bool> ApproveCourseRequirementAsync(long id, int approvalStatus, long approvedBy);
        Task<bool> ApproveSchoolAsync(long schoolId, int approvalStatus, long approvedBy);



        //Task<bool> ApproveUniversityAsync(long id, int approvalStatus, long approvedBy);

    }
}
