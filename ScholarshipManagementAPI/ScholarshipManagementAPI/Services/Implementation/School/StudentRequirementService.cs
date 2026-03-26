using Microsoft.EntityFrameworkCore;
using ScholarshipManagementAPI.Data.Contexts;
using ScholarshipManagementAPI.Data.DbModels;
using ScholarshipManagementAPI.DTOs.Common.Response;
using ScholarshipManagementAPI.DTOs.School.MasterSchool;
using ScholarshipManagementAPI.DTOs.School.StudentRequirements;
using ScholarshipManagementAPI.Helper.Enums;
using ScholarshipManagementAPI.Helper.Utilities;
using ScholarshipManagementAPI.Services.Interface.School;

namespace ScholarshipManagementAPI.Services.Implementation.School
{
    public class StudentRequirementService : IStudentRequirementService
    {
        private readonly AppDbContext _context;

        public StudentRequirementService(AppDbContext context)
        {
            _context = context;
        }


        // ---------------- StudentRequirementMap ----------------
        public async Task<long> CreateStudentRequirementMapAsync(StudentRequirementMappingDto dto)
        {
            if (dto.StudentID <= 0)
                throw new CustomException("Invalid student.");

            if (dto.ReqId <= 0)
                throw new CustomException("Invalid requirement.");

            // Check: Student already has a request
            var exists = await _context.StudentReqLists
                .Include(x => x.Req)
                .ThenInclude(r => r.Course)
                .AnyAsync(x => x.StudentId == dto.StudentID && x.Req.Course.UniversityId == dto.UniversityId);

            if (exists)
            {
                throw new CustomException("Student already applied in this university.");
            }

            var entity = new StudentReqList
            {
                StudentId = dto.StudentID,
                ReqId = dto.ReqId,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate
            };

            _context.StudentReqLists.Add(entity);
            await _context.SaveChangesAsync();

            return entity.StudentReqId;
        }



        public async Task<bool> UpdateStudentRequirementMapAsync(StudentRequirementMappingDto dto)
        {
            var entity = await _context.StudentReqLists
                .FirstOrDefaultAsync(x => x.StudentReqId == dto.StudentReqID);

            if (entity == null)
                throw new CustomException("Record not found.");

            // Optional: prevent duplicate again
            var exists = await _context.StudentReqLists
                .Include(x => x.Req)
                .ThenInclude(r => r.Course)
                .AnyAsync(x => x.StudentId == dto.StudentID && x.Req.Course.UniversityId == dto.UniversityId && x.StudentReqId != dto.StudentReqID);

            if (exists)
                throw new CustomException("Student already applied in this university.");

            // studentId is not updatable, 
            // entity.StudentId = dto.StudentID;
            entity.ReqId = dto.ReqId;

            await _context.SaveChangesAsync();

            return true;
        }


        public async Task<bool> UpdateStudentRequirementMapByUniversityAsync(StudentRequirementRequestDto dto)
        {
            var entity = await _context.StudentReqLists
                .FirstOrDefaultAsync(x => x.StudentReqId == dto.StudentReqID);

            if (entity == null)
                return false;

            // Optional: prevent duplicate again
            var exists = await _context.StudentReqLists
                .Include(x => x.Req)
                .ThenInclude(r => r.Course)
                .AnyAsync(x => x.StudentId == dto.StudentID 
                               && x.Req.Course.UniversityId == dto.UniversityId 
                               && x.StudentReqId != dto.StudentReqID);

            if (exists)
                throw new CustomException("Student already applied in this university.");

            // studentId is not updatable, 
            // entity.StudentId = dto.StudentID;
            // entity.ReqId = dto.ReqId;

            entity.UniAdmissionStatus = dto.UniAdmissionStatus;
            entity.SemesterStartDate = dto.SemesterStartDate;
            entity.LetterAccepCode = dto.LetterAccepCode;
            entity.UniAwardingstatus = dto.UniAwardingStatus;

            await _context.SaveChangesAsync();

            return true;
        }




        // ---------------- DELETE ----------------
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.StudentReqLists
                .FirstOrDefaultAsync(x => x.StudentReqId == id);

            if (entity == null)
                return false;

            _context.StudentReqLists.Remove(entity);

            await _context.SaveChangesAsync();

            return true;
        }



        // ---------------- GET BY ID ----------------
        public async Task<StudentRequirementRequestDto?> GetByIdAsync(long id)
        {
            return await _context.StudentReqLists
                .Include(x => x.Student)
                .Include(x => x.Req)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.CourseType)
                .ThenInclude(x => x.University)
                .AsNoTracking()
                .Where(x => x.StudentReqId == id)
                .Select(x => new StudentRequirementRequestDto
                {
                    StudentReqID = x.StudentReqId,
                    StudentID = x.StudentId,
                    ReqId = x.ReqId,
                    UniAdmissionStatus = x.UniAdmissionStatus,
                    ReasonRejection = x.ReasonRejection,
                    MissedDocuments = x.MissedDocuments,
                    SemesterStartDate = x.SemesterStartDate,
                    LetterAccepCode = x.LetterAccepCode,
                    UniStatusBy = x.UniStatusBy,
                    UniStatusDate = x.UniStatusDate,
                    DaAdmissionStatus = x.DaAdmissionStatus,
                    DaStatusBy = x.DaStatusBy,
                    DaStatusDate = x.DaStatusDate,
                    DonorId = x.DonorId,
                    TotalCost = x.TotalCost,
                    CreateEmailBy = x.CreateEmailBy,
                    ReasonInProgress = x.ReasonInProgress,
                    UniAwardingStatus = x.UniAwardingstatus,
                    UniAwardingStatusCost = x.UniAwardingstatusCost,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    AcademicYear = x.Req != null ? x.Req.AcademicYear : null,
                    StudentFullName = x.Student != null ? $"{x.Student.StudentSalutation} {x.Student.StudentFirstName} {x.Student.StudentLastName}" : null,
                    StudentNumber = x.Student != null ? x.Student.StudentNumber : null,
                    StudentPhoto = x.Student != null ? x.Student.Photo : null,
                    CourseId = x.Req != null && x.Req.Course != null ? x.Req.Course.CourseId : (long?)null,
                    CourseName = x.Req != null && x.Req.Course != null ? x.Req.Course.CourseName : null,
                    CourseTypeId = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null ? x.Req.Course.CourseType.CourseTypeId : (long?)null,
                    CourseTypeName = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null ? x.Req.Course.CourseType.CourseTypeName : null,
                    UniversityId = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null && x.Req.Course.CourseType.University != null ? x.Req.Course.CourseType.University.UniversityId : (long?)null,
                    UniversityName = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null && x.Req.Course.CourseType.University != null ? x.Req.Course.CourseType.University.UniversityName : null,
                    RequiredDocuments = x.Req != null ? x.Req.RequiredDocuments : null
                })
                .FirstOrDefaultAsync();
        }



        // ---------------- GET ALL FILTER ----------------
        public async Task<PagedResultDto<StudentRequirementRequestDto>> GetByFilterAsync(StudentRequirementFilterDto filter)
        {
            var query = _context.StudentReqLists
                .Include(x => x.Student)
                .Include(x => x.Req)
                .ThenInclude(x => x.Course)
                .ThenInclude(x => x.CourseType)
                .ThenInclude(x => x.University)
                .AsNoTracking()
                .AsQueryable();

            // Student filter
            if (filter.StudentID.HasValue)
            {
                query = query.Where(x => x.StudentId == filter.StudentID.Value);
            }

            // Request filter
            if (filter.ReqId.HasValue)
            {
                query = query.Where(x => x.ReqId == filter.ReqId.Value);
            }

            // University Admission Status
            if (filter.UniAdmissionStatus.HasValue)
            {
                query = query.Where(x => x.UniAdmissionStatus == filter.UniAdmissionStatus.Value);
            }

            // DA Admission Status
            if (filter.DaAdmissionStatus.HasValue)
            {
                query = query.Where(x => x.DaAdmissionStatus == filter.DaAdmissionStatus.Value);
            }

            // Awarding Status
            if (filter.UniAwardingStatus.HasValue)
            {
                query = query.Where(x => x.UniAwardingstatus == filter.UniAwardingStatus.Value);
            }

            // Donor filter
            if (filter.DonorId.HasValue)
            {
                query = query.Where(x => x.DonorId == filter.DonorId.Value);
            }


            // Created Date range
            if (filter.CreatedFrom.HasValue || filter.CreatedTo.HasValue)
            {
                var from = filter.CreatedFrom ?? DateTime.MinValue;
                var to = filter.CreatedTo ?? DateTime.MaxValue;

                query = query.Where(x =>
                    x.CreatedDate >= from &&
                    x.CreatedDate <= to
                );
            }

            // Semester Start Date range
            if (filter.SemesterStartFrom.HasValue || filter.SemesterStartTo.HasValue)
            {
                var from = filter.SemesterStartFrom ?? DateTime.MinValue;
                var to = filter.SemesterStartTo ?? DateTime.MaxValue;

                query = query.Where(x =>
                    x.SemesterStartDate.HasValue &&
                    x.SemesterStartDate.Value >= from &&
                    x.SemesterStartDate.Value <= to
                );
            }


            // Cost
            if (filter.MinTotalCost.HasValue)
            {
                query = query.Where(x => x.TotalCost.HasValue && x.TotalCost.Value >= filter.MinTotalCost.Value);
            }

            if (filter.MaxTotalCost.HasValue)
            {
                query = query.Where(x => x.TotalCost.HasValue && x.TotalCost.Value <= filter.MaxTotalCost.Value);
            }



            /* Global Search */
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();

                query = query.Where(x =>
                    (x.ReasonRejection != null && x.ReasonRejection.ToLower().Contains(search)) ||
                    (x.MissedDocuments != null && x.MissedDocuments.ToLower().Contains(search)) ||
                    (x.CreateEmailBy != null && x.CreateEmailBy.ToLower().Contains(search)) ||
                    (x.ReasonInProgress != null && x.ReasonInProgress.ToLower().Contains(search)) ||

                    // Related data
                    (x.Student != null &&
                        ((x.Student.StudentFirstName + " " + x.Student.StudentLastName).ToLower().Contains(search))) ||

                    (x.Req != null && x.Req.Course != null &&
                        x.Req.Course.CourseName.ToLower().Contains(search))
                );
            }


            // ---------- Total Count (before pagination) ----------
            var totalCount = await query.CountAsync();

            // ---------- Ordering ----------
            query = query.OrderBy(x => x.StudentReqId);

            // ---------- Pagination rule ----------
            if (filter.PageSize > 0)
            {
                query = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            var items = await query
                .Select(x => new StudentRequirementRequestDto
                {
                    StudentReqID = x.StudentReqId,
                    StudentID = x.StudentId,
                    ReqId = x.ReqId,
                    UniAdmissionStatus = x.UniAdmissionStatus,
                    ReasonRejection = x.ReasonRejection,
                    MissedDocuments = x.MissedDocuments,
                    SemesterStartDate = x.SemesterStartDate,
                    LetterAccepCode = x.LetterAccepCode,
                    UniStatusBy = x.UniStatusBy,
                    UniStatusDate = x.UniStatusDate,
                    DaAdmissionStatus = x.DaAdmissionStatus,
                    DaStatusBy = x.DaStatusBy,
                    DaStatusDate = x.DaStatusDate,
                    DonorId = x.DonorId,
                    TotalCost = x.TotalCost,
                    CreateEmailBy = x.CreateEmailBy,
                    ReasonInProgress = x.ReasonInProgress,
                    UniAwardingStatus = x.UniAwardingstatus,
                    UniAwardingStatusCost = x.UniAwardingstatusCost,
                    CreatedBy = x.CreatedBy,
                    CreatedDate = x.CreatedDate,
                    AcademicYear = x.Req != null ? x.Req.AcademicYear : null,
                    StudentFullName = x.Student != null ? $"{x.Student.StudentSalutation} {x.Student.StudentFirstName} {x.Student.StudentLastName}" : null,
                    StudentNumber = x.Student != null ? x.Student.StudentNumber : null,
                    StudentPhoto = x.Student != null ? x.Student.Photo : null,
                    CourseId = x.Req != null && x.Req.Course != null ? x.Req.Course.CourseId : (long?)null,
                    CourseName = x.Req != null && x.Req.Course != null ? x.Req.Course.CourseName : null,
                    CourseTypeId = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null ? x.Req.Course.CourseType.CourseTypeId : (long?)null,
                    CourseTypeName = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null ? x.Req.Course.CourseType.CourseTypeName : null,
                    UniversityId = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null && x.Req.Course.CourseType.University != null ? x.Req.Course.CourseType.University.UniversityId : (long?)null,
                    UniversityName = x.Req != null && x.Req.Course != null && x.Req.Course.CourseType != null && x.Req.Course.CourseType.University != null ? x.Req.Course.CourseType.University.UniversityName : null,
                    RequiredDocuments = x.Req != null ? x.Req.RequiredDocuments : null
                })
                .ToListAsync();

            return new PagedResultDto<StudentRequirementRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }



    }
}
