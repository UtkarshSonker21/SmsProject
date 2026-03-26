using Microsoft.EntityFrameworkCore;
using ScholarshipManagementAPI.Data.Contexts;
using ScholarshipManagementAPI.Data.DbModels;
using ScholarshipManagementAPI.DTOs.Common.Auth;
using ScholarshipManagementAPI.DTOs.Common.Response;
using ScholarshipManagementAPI.DTOs.School.StudentRequirements;
using ScholarshipManagementAPI.DTOs.School.Students;
using ScholarshipManagementAPI.DTOs.University.CourseRequirement;
using ScholarshipManagementAPI.DTOs.University.MasterCourse;
using ScholarshipManagementAPI.Helper;
using ScholarshipManagementAPI.Helper.Enums;
using ScholarshipManagementAPI.Helper.Utilities;
using ScholarshipManagementAPI.Services.Interface.University;
using System.Text.Json;

namespace ScholarshipManagementAPI.Services.Implementation.University
{
    public class CourseRequirementService : ICourseRequirementService
    {
        private readonly AppDbContext _context;
        public CourseRequirementService(AppDbContext context)
        {
            _context = context;
        }


        // ---------------- CREATE ----------------
        public async Task<long> CreateAsync(CourseRequirementRequestDto dto)
        {

            var entity = new UnCourseReq
            {
                CourseId = dto.CourseId,
                AcademicYear = dto.AcademicYear,

                RequiredDocuments = dto.RequiredDocuments,
                HsDivision = dto.HsDivision,

                NoSeats = dto.NoSeats,
                CollegeScore = dto.CollegeScore,
                TzStuCombi = dto.TzStuCombi,

                RegistrationCost = dto.RegistrationCost,
                TutionCost = dto.TutionCost,
                TextBookCost = dto.TextBookCost,
                AccomoCost = dto.AccomoCost,
                TravellingCost = dto.TravellingCost,
                TransportCost = dto.TransportCost,
                DocuAttestCost = dto.DocuAttestCost,
                VisaResiCost = dto.VisaResiCost,

                ReqStartDate = dto.ReqStartDate,
                ReqEndDate = dto.ReqEndDate,

                ApprovalStatus = (int)ApprovalStatus.Pending,  // pending by default
                ApprovedBy = null,                             // no approver at creation
                IsActive = true,                               // default to active
                CreatedDate = dto.CreatedDate,                 // always server-side
                CreatedBy = dto.CreatedBy                      // always server-side
            };

            _context.UnCourseReqs.Add(entity);
            await _context.SaveChangesAsync();

            return entity.ReqId;
        }



        // ---------------- UPDATE ----------------
        public async Task<bool> UpdateAsync(CourseRequirementRequestDto dto)
        {
            var entity = await _context.UnCourseReqs
                .FirstOrDefaultAsync(x => x.ReqId == dto.ReqId);

            if (entity == null)
            {
                throw new CustomException("Course Requirement not found");
            }

            // ⭐ BUSINESS RULE CHECK (place here)
            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved)
                throw new CustomException("Approved course requirement cannot be edited");

            // CourseId usually should NOT be changed
            // entity.CourseId = dto.CourseId;

            entity.AcademicYear = dto.AcademicYear;

            entity.RequiredDocuments = dto.RequiredDocuments;
            entity.HsDivision = dto.HsDivision;

            entity.NoSeats = dto.NoSeats;
            entity.CollegeScore = dto.CollegeScore;
            entity.TzStuCombi = dto.TzStuCombi;

            entity.RegistrationCost = dto.RegistrationCost;
            entity.TutionCost = dto.TutionCost;
            entity.TextBookCost = dto.TextBookCost;
            entity.AccomoCost = dto.AccomoCost;
            entity.TravellingCost = dto.TravellingCost;
            entity.TransportCost = dto.TransportCost;
            entity.DocuAttestCost = dto.DocuAttestCost;
            entity.VisaResiCost = dto.VisaResiCost;

            entity.ReqStartDate = dto.ReqStartDate;
            entity.ReqEndDate = dto.ReqEndDate;

            // entity.IsActive = dto.IsActive;

            // entity.ApprovalStatus = dto.ApprovalStatus;   // usually updated via ngo
            // entity.ApprovedBy = dto.ApprovedBy;           // usually updated via ngo
            // entity.CreatedDate = dto.CreatedDate;         // usually not updated
            // entity.CreatedBy = dto.CreatedBy;             // usually not updated

            await _context.SaveChangesAsync();

            return true;
        }



        // ---------------- DELETE ----------------
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.UnCourseReqs
                .FirstOrDefaultAsync(x => x.ReqId == id && x.IsActive == true);

            if (entity == null)
                return false;


            if (entity.ApprovalStatus == (int)ApprovalStatus.Approved)
            {
                throw new CustomException("Approved course requirement cannot be deleted");
            }

            // Soft delete
            entity.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }



        // ---------------- GET BY ID ----------------
        public async Task<CourseRequirementRequestDto?> GetByIdAsync(long id)
        {
            return await _context.UnCourseReqs
                .AsNoTracking()
                .Where(x => x.ReqId == id)
                .Include(x => x.Course)
                .Select(x => new CourseRequirementRequestDto
                {
                    ReqId = x.ReqId,
                    CourseId = x.CourseId,
                    AcademicYear = x.AcademicYear,

                    RequiredDocuments = x.RequiredDocuments,
                    HsDivision = x.HsDivision,

                    NoSeats = x.NoSeats,
                    CollegeScore = x.CollegeScore,
                    TzStuCombi = x.TzStuCombi,

                    RegistrationCost = x.RegistrationCost,
                    TutionCost = x.TutionCost,
                    TextBookCost = x.TextBookCost,
                    AccomoCost = x.AccomoCost,
                    TravellingCost = x.TravellingCost,
                    TransportCost = x.TransportCost,
                    DocuAttestCost = x.DocuAttestCost,
                    VisaResiCost = x.VisaResiCost,

                    ReqStartDate = x.ReqStartDate,
                    ReqEndDate = x.ReqEndDate,

                    IsActive = x.IsActive ?? false,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,

                    CourseName = x.Course.CourseName,
                    UniversityName = x.Course.University.UniversityName,

                    ApprovalStatus = x.ApprovalStatus,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedByName = x.ApprovedByNavigation != null ? x.ApprovedByNavigation.LoginName : null
                })
                .FirstOrDefaultAsync();
        }



        // ---------------- GET ALL FILTER ----------------
        public async Task<PagedResultDto<CourseRequirementRequestDto>> GetByFilterAsync(CourseRequirementFilterDto filter, LoggedInUserDto currentUser)
        {
            var query = _context.UnCourseReqs
                .AsNoTracking()
                .Include(x => x.Course) 
                .AsQueryable();

            // ---------- DATA SCOPE FILTER ----------
            if (currentUser.StaffType != StaffType.SuperAdmin &&
                currentUser.StaffType != StaffType.Ngo)
            {
                if (currentUser.StaffType == StaffType.University)
                {
                    query = query.Where(x => x.Course.UniversityId == currentUser.UniversityId);
                }
                else if (currentUser.StaffType == StaffType.School)
                {
                    // School should only view course requirements
                    query = query.Where(x => true);
                }
            }

            // UniversityId filter
            if (filter.UniversityId.HasValue)
            {
                query = query.Where(x => x.Course.UniversityId == filter.UniversityId.Value);
            }

            // CourseId filter
            if (filter.CourseId.HasValue)
            {
                query = query.Where(x => x.CourseId == filter.CourseId.Value);
            }

            // Approved filter
            if (filter.ApprovalStatus.HasValue)
            {
                query = query.Where(x => x.ApprovalStatus == filter.ApprovalStatus.Value);
            }

            // Active status filter
            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }


            // Date range filter (OVERLAP LOGIC)
            if (filter.ReqStartDate.HasValue)
            {
                var startDate = filter.ReqStartDate.Value.Date;

                query = query.Where(x =>
                    x.ReqEndDate == null || x.ReqEndDate.Value.Date >= startDate
                );
            }

            if (filter.ReqEndDate.HasValue)
            {
                var endDate = filter.ReqEndDate.Value.Date;

                query = query.Where(x =>
                    x.ReqStartDate == null || x.ReqStartDate.Value.Date <= endDate
                );
            }


            /* Global Search */
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();
                query = query.Where(x =>
                    x.Course.CourseName.ToLower().Contains(search) 
                );
            }


            // ---------- Total Count (before pagination) ----------
            var totalCount = await query.CountAsync();

            // ---------- Ordering ----------
            query = query.OrderBy(x => x.ReqId);

            // ---------- Pagination rule ----------
            if (filter.PageSize > 0)
            {
                query = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            var items = await query
                .Select(x => new CourseRequirementRequestDto
                {
                    ReqId = x.ReqId,
                    CourseId = x.CourseId,
                    AcademicYear = x.AcademicYear,

                    RequiredDocuments = x.RequiredDocuments,
                    HsDivision = x.HsDivision,

                    NoSeats = x.NoSeats,
                    CollegeScore = x.CollegeScore,
                    TzStuCombi = x.TzStuCombi,

                    RegistrationCost = x.RegistrationCost,
                    TutionCost = x.TutionCost,
                    TextBookCost = x.TextBookCost,
                    AccomoCost = x.AccomoCost,
                    TravellingCost = x.TravellingCost,
                    TransportCost = x.TransportCost,
                    DocuAttestCost = x.DocuAttestCost,
                    VisaResiCost = x.VisaResiCost,

                    ReqStartDate = x.ReqStartDate,
                    ReqEndDate = x.ReqEndDate,

                    IsActive = x.IsActive ?? false,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,

                    CourseName = x.Course.CourseName,
                    UniversityName = x.Course.University.UniversityName,

                    ApprovalStatus = x.ApprovalStatus,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedByName = x.ApprovedByNavigation != null ? x.ApprovedByNavigation.LoginName : null
                })
                .ToListAsync();

            return new PagedResultDto<CourseRequirementRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }



        // ---------------- GET ALL Enrollments ----------------
        public async Task<PagedResultDto<CourseRequirementEnrollmentDto>> GetEnrollmentsAsync(CourseRequirementFilterDto filter, LoggedInUserDto currentUser)
        {
            var query = _context.UnCourseReqs
                .AsNoTracking()
                .Include(x => x.Course)
                .ThenInclude(c => c.University)
                .AsQueryable();

            // ---------- DATA SCOPE FILTER ----------
            if (currentUser.StaffType != StaffType.SuperAdmin &&
                currentUser.StaffType != StaffType.Ngo)
            {
                if (currentUser.StaffType == StaffType.University)
                {
                    query = query.Where(x => x.Course.UniversityId == currentUser.UniversityId);
                }
            }

            // ---------- FILTERS ----------
            if (filter.UniversityId.HasValue)
                query = query.Where(x => x.Course.UniversityId == filter.UniversityId.Value);

            if (filter.CourseId.HasValue)
                query = query.Where(x => x.CourseId == filter.CourseId.Value);

            if (filter.ApprovalStatus.HasValue)
                query = query.Where(x => x.ApprovalStatus == filter.ApprovalStatus.Value);

            if (filter.IsActive.HasValue)
                query = query.Where(x => x.IsActive == filter.IsActive.Value);

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();
                query = query.Where(x =>
                    x.Course.CourseName.ToLower().Contains(search));
            }

            // ---------- TOTAL COUNT ----------
            var totalCount = await query.CountAsync();

            // ---------- PAGINATION ----------
            query = query.OrderBy(x => x.ReqId);

            if (filter.PageSize > 0)
            {
                query = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            // ---------- SELECT (ENROLLMENT DATA) ----------
            var items = await query
                .Select(x => new CourseRequirementEnrollmentDto
                {
                    ReqId = x.ReqId,

                    CourseName = x.Course.CourseName,
                    UniversityName = x.Course.University.UniversityName,

                    AcademicYear = x.AcademicYear,
                    Seats = x.NoSeats,

                    ReqStartDate = x.ReqStartDate,
                    ReqEndDate = x.ReqEndDate,

                    // Student Count
                    StudentCount = _context.StudentReqLists
                        .Count(s => s.ReqId == x.ReqId),

                    // Remaining Seats
                    RemainingSeats = (x.NoSeats) -
                        _context.StudentReqLists.Count(s => s.ReqId == x.ReqId),

                    RequiredDocuments = x.RequiredDocuments,

                    TotalCost =
                        (x.RegistrationCost ?? 0) +
                        (x.TutionCost ?? 0) +
                        (x.TextBookCost ?? 0) +
                        (x.AccomoCost ?? 0) +
                        (x.TravellingCost ?? 0) +
                        (x.TransportCost ?? 0) +
                        (x.DocuAttestCost ?? 0) +
                        (x.VisaResiCost ?? 0)
                })
                .ToListAsync();

            return new PagedResultDto<CourseRequirementEnrollmentDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }




        // ---------------- GET ALL Enrolled Students ----------------
        public async Task<PagedResultDto<EnrolledStudentDto>> GetEnrolledStudentsAsync(long reqId, StudentFilterDto filter)
        {
            var query = _context.StudentReqLists
                .AsNoTracking()
                .Where(x => x.ReqId == reqId)
                .Include(x => x.Student)
                    .ThenInclude(s => s.School)
                .AsQueryable();

            // ---------- Filters ----------
            if (filter.SchoolId.HasValue)
            {
                query = query.Where(x => x.Student.SchoolId == filter.SchoolId.Value);
            }

            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();

                query = query.Where(x =>
                    x.Student.StudentNumber.ToLower().Contains(search) ||
                    x.Student.StudentFirstName.ToLower().Contains(search) ||
                    (x.Student.StudentLastName != null && x.Student.StudentLastName.ToLower().Contains(search)) ||
                    (x.Student.EmailId != null && x.Student.EmailId.ToLower().Contains(search))
                );
            }

            // ---------- Count ----------
            var totalCount = await query.CountAsync();

            // ---------- Paging ----------
            query = query.OrderByDescending(x => x.CreatedDate);

            if (filter.PageSize > 0)
            {
                query = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            // ---------- Projection ----------
            var items = await query
                .Select(x => new EnrolledStudentDto
                {
                    StudentId = x.StudentId,

                    StudentFullName = x.Student.StudentFirstName + " " + (x.Student.StudentLastName ?? ""),
                    StudentNumber = x.Student.StudentNumber,
                    StudentPhoto = x.Student.Photo,

                    Gender = x.Student.Gender,
                    DateOfBirth = x.Student.DateOfBirth,

                    StudentEmail = x.Student.EmailId,
                    StudentMobileNo = x.Student.MobileNo,

                    Address = x.Student.Address,
                    AddressCity = x.Student.AddressCity,
                    MasterState = x.Student.MasterState,
                    MasterCountry = x.Student.MasterCountry,
                    ZipCode = x.Student.ZipCode,


                    SchoolName = x.Student.School != null
                        ? x.Student.School.SchoolName
                        : null,

                    ShortName = x.Student.School != null
                        ? x.Student.School.ShortName
                        : null,

                    SchoolEmail = x.Student.School != null ? x.Student.School.EmailId : null,
                    SchoolMobileNo = x.Student.School != null ? x.Student.School.SchoolPhoneNo : null,
                    SchoolWebsite = x.Student.School != null ? x.Student.School.SchoolWebsite : null,

                    // Status mapping
                    DocStatus = x.UniAdmissionStatus == 1 ? "Accepted" :
                                x.UniAdmissionStatus == 2 ? "Rejected" : "Pending",

                    AwardingStatus = x.UniAwardingstatus == 1 ? "Awarded" : "Pending",

                    SponsoredStatus = x.DonorId != null ? "Sponsored" : "Not Sponsored",

                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate
                })
                .ToListAsync();

            return new PagedResultDto<EnrolledStudentDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }


    }
}
