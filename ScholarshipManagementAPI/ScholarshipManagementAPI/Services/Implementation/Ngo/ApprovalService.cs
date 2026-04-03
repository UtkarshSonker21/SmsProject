using Microsoft.EntityFrameworkCore;
using ScholarshipManagementAPI.Data.Contexts;
using ScholarshipManagementAPI.Helper;
using ScholarshipManagementAPI.Helper.Enums;
using ScholarshipManagementAPI.Helper.Utilities;
using ScholarshipManagementAPI.Services.Interface.Ngo;

namespace ScholarshipManagementAPI.Services.Implementation.Ngo
{
    public class ApprovalService : IApprovalService
    {
        private readonly AppDbContext _context;
        public ApprovalService(AppDbContext context)
        {
            _context = context;
        }


        public async Task<bool> ApproveCourseTypeAsync(long id , int approvalStatus, long approvedBy)
        {
            var entity = await _context.UnMasterCourseTypes
                .FirstOrDefaultAsync(x => x.CourseTypeId == id);

            if (entity == null)
            {
                throw new CustomException("Course type not found");
            }

            // validate enum
            if (!Enum.IsDefined(typeof(ApprovalStatus), approvalStatus))
                throw new CustomException("Invalid approval status");

            // block pending
            if (approvalStatus == (int)ApprovalStatus.Pending)
                throw new CustomException("Pending status cannot be set from approval action");

            // prevent re-approval
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Approved)
                throw new CustomException("Course type already approved");

            // prevent re-reject 
            if (entity.ApprovalStatus == (int)ApprovalStatus.Rejected &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Course type already rejected");

            // prevent reject after approve
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Approved course type cannot be rejected");

            // usually updated via ngo
            entity.ApprovalStatus = approvalStatus;
            entity.ApprovedBy = approvedBy;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> ApproveCourseAsync(long id, int approvalStatus, long approvedBy)
        {
            var entity = await _context.UnMasterCourses
                .FirstOrDefaultAsync(x => x.CourseId == id);

            if (entity == null)
            {
                throw new CustomException("Course not found");
            }

            // validate enum
            if (!Enum.IsDefined(typeof(ApprovalStatus), approvalStatus))
                throw new CustomException("Invalid approval status");

            // block pending
            if (approvalStatus == (int)ApprovalStatus.Pending)
                throw new CustomException("Pending status cannot be set from approval action");

            // prevent re-approval
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Approved)
                throw new CustomException("Course already approved");

            // prevent re-reject 
            if (entity.ApprovalStatus == (int)ApprovalStatus.Rejected &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Course already rejected");

            // prevent reject after approve
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Approved course cannot be rejected");


            // usually updated via ngo
            entity.ApprovalStatus = approvalStatus;
            entity.ApprovedBy = approvedBy;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> ApproveCourseRequirementAsync(long id, int approvalStatus, long approvedBy)
        {
            var entity = await _context.UnCourseReqs
                .FirstOrDefaultAsync(x => x.ReqId == id);

            if (entity == null)
            {
                throw new CustomException("Course requirement not found");
            }

            // validate enum
            if (!Enum.IsDefined(typeof(ApprovalStatus), approvalStatus))
                throw new CustomException("Invalid approval status");

            // block pending
            if (approvalStatus == (int)ApprovalStatus.Pending)
                throw new CustomException("Pending status cannot be set from approval action");

            // prevent re-approval
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Approved)
                throw new CustomException("Course requirement already approved");

            // prevent re-reject 
            if (entity.ApprovalStatus == (int)ApprovalStatus.Rejected &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Course requirement already rejected");

            // prevent reject after approve
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Approved course requirement cannot be rejected");

            // usually updated via ngo
            entity.ApprovalStatus = approvalStatus;
            entity.ApprovedBy = approvedBy;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> ApproveSchoolAsync(long id, int approvalStatus, long approvedBy)
        {
            var entity = await _context.MasterSchoolLists
                .FirstOrDefaultAsync(x => x.SchoolId == id);

            if (entity == null)
                throw new CustomException("School not found");

            // validate enum
            if (!Enum.IsDefined(typeof(ApprovalStatus), approvalStatus))
                throw new CustomException("Invalid approval status");

            // block pending
            if (approvalStatus == (int)ApprovalStatus.Pending)
                throw new CustomException("Pending status cannot be set from approval action");

            // prevent re-approval
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Approved)
                throw new CustomException("School already approved");

            // prevent re-reject 
            if (entity.ApprovalStatus == (int)ApprovalStatus.Rejected &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("School already rejected");

            // prevent reject after approve
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
                approvalStatus == (int)ApprovalStatus.Rejected)
                throw new CustomException("Approved school cannot be rejected");

            // usually updated via ngo
            entity.ApprovalStatus = approvalStatus;
            entity.ApprovedBy = approvedBy;

            await _context.SaveChangesAsync();

            return true;
        }


        //public async Task<bool> ApproveUniversityAsync(long id, int approvalStatus, long approvedBy)
        //{
        //    var entity = await _context.UnUniversityLists
        //        .FirstOrDefaultAsync(x => x.UniversityId == id);

        //    if (entity == null)
        //        throw new CustomException("University not found");

        //    // validate enum
        //    if (!Enum.IsDefined(typeof(ApprovalStatus), approvalStatus))
        //        throw new CustomException("Invalid approval status");

        //    // block pending
        //    if (approvalStatus == (int)ApprovalStatus.Pending)
        //        throw new CustomException("Pending status cannot be set from approval action");

        //    // prevent re-approval
        //    if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
        //        approvalStatus == (int)ApprovalStatus.Approved)
        //        throw new CustomException("University already approved");

        //    // prevent re-reject 
        //    if (entity.ApprovalStatus == (int)ApprovalStatus.Rejected &&
        //        approvalStatus == (int)ApprovalStatus.Rejected)
        //        throw new CustomException("University already rejected");

        //    // prevent reject after approve
        //    if (entity.ApprovalStatus == (int)ApprovalStatus.Approved &&
        //        approvalStatus == (int)ApprovalStatus.Rejected)
        //        throw new CustomException("Approved University cannot be rejected");

        //    // usually updated via ngo
        //    entity.ApprovalStatus = approvalStatus;
        //    entity.ApprovedBy = approvedBy;

        //    await _context.SaveChangesAsync();

        //    return true;
        //}



    }
}
