using Microsoft.EntityFrameworkCore;
using ScholarshipManagementAPI.Data.Contexts;
using ScholarshipManagementAPI.Data.DbModels;
using ScholarshipManagementAPI.DTOs.Common.Response;
using ScholarshipManagementAPI.DTOs.School.MasterSchool;
using ScholarshipManagementAPI.DTOs.SuperAdmin.UsersMenu;
using ScholarshipManagementAPI.Helper;
using ScholarshipManagementAPI.Helper.Enums;
using ScholarshipManagementAPI.Helper.Utilities;
using ScholarshipManagementAPI.Services.Interface.School;

namespace ScholarshipManagementAPI.Services.Implementation.School
{
    public class MasterSchoolService : IMasterSchoolService
    {
        private readonly AppDbContext _context;

        public MasterSchoolService(AppDbContext context)
        {
            _context = context;
        }


        // ---------------- CREATE ----------------
        public async Task<long> CreateAsync(MasterSchoolRequestDto dto)
        {
            if (await _context.MasterSchoolLists
                .AnyAsync(x => x.SchoolName.ToLower() == dto.SchoolName.ToLower()))
            {
                throw new CustomException("School with same name already exists");
            }

            var entity = new MasterSchoolList
            {
                SchoolName = dto.SchoolName,
                StudentCodeFormatPrefix = dto.StudentCodeFormatPrefix,
                StudentCodeFormatSufix = dto.StudentCodeFormatSufix,
                StudentCodeFormatStartingNo = dto.StudentCodeFormatStartingNo,
                StudentCodeFormatLastSavedNumber = dto.StudentCodeFormatLastSavedNumber,
                CountryId = dto.CountryId,
                ShortName = dto.ShortName,
                Area = dto.Area,
                CenterName = dto.CenterName,
                SchoolNumber = dto.SchoolNumber,
                SchoolType = dto.SchoolType,
                SchoolTeachingLanguage = dto.SchoolTeachingLanguage,
                GraduatesEnglishLessThan = dto.GraduatesEnglishLessThan,
                TotalNumberOfHighSchoolLevel = dto.TotalNumberOfHighSchoolLevel,
                AverageNumberOfStudentPerClass = dto.AverageNumberOfStudentPerClass,
                SchoolAccreditations = dto.SchoolAccreditations,
                SchoolSubjectCurriculum = dto.SchoolSubjectCurriculum,
                StudentsuccessAverage = dto.StudentSuccessAverage,
                AverageSchoolGraduates = dto.AverageSchoolGraduates,
                SchoolLocalRank = dto.SchoolLocalRank,
                OwningInstitution = dto.OwningInstitution,
                SchoolWebsite = dto.SchoolWebsite,
                SchoolPhoneNo = dto.SchoolPhoneNo,
                EmailId = dto.EmailId,
                IsActive = dto.IsActive,
                AcademicyearStartDate = dto.AcademicYearStartDate,
                AcademicyearEndDate = dto.AcademicYearEndDate,

                ApprovalStatus = (int)ApprovalStatus.Pending,  // pending by default
                ApprovedBy = null,                             // no approver by default

                CreatedDate = DateTime.UtcNow,     // always server-side
            };

            _context.MasterSchoolLists.Add(entity);
            await _context.SaveChangesAsync();

            return entity.SchoolId;
        }


        // ---------------- UPDATE ----------------
        public async Task<bool> UpdateAsync(MasterSchoolRequestDto dto)
        {
            if (dto.SchoolId == null || dto.SchoolId == 0)
                return false;

            if (await _context.MasterSchoolLists.AnyAsync(x =>
                      x.SchoolName.ToLower() == dto.SchoolName.ToLower()
                      && x.SchoolId != dto.SchoolId))
            {
                throw new CustomException("School with same name already exists");
            }

            var entity = await _context.MasterSchoolLists
                .FirstOrDefaultAsync(x => x.SchoolId == dto.SchoolId);

            if (entity == null)
                return false;

            //SchoolId = dto.SchoolId.Value,
            entity.SchoolName = dto.SchoolName;
            entity.StudentCodeFormatPrefix = dto.StudentCodeFormatPrefix;
            entity.StudentCodeFormatSufix = dto.StudentCodeFormatSufix;
            entity.StudentCodeFormatStartingNo = dto.StudentCodeFormatStartingNo;
            entity.StudentCodeFormatLastSavedNumber = dto.StudentCodeFormatLastSavedNumber;
            entity.CountryId = dto.CountryId;
            entity.ShortName = dto.ShortName;
            entity.Area = dto.Area;
            entity.CenterName = dto.CenterName;
            entity.SchoolNumber = dto.SchoolNumber;
            entity.SchoolType = dto.SchoolType;
            entity.SchoolTeachingLanguage = dto.SchoolTeachingLanguage;
            entity.GraduatesEnglishLessThan = dto.GraduatesEnglishLessThan;
            entity.TotalNumberOfHighSchoolLevel = dto.TotalNumberOfHighSchoolLevel;
            entity.AverageNumberOfStudentPerClass = dto.AverageNumberOfStudentPerClass;
            entity.SchoolAccreditations = dto.SchoolAccreditations;
            entity.SchoolSubjectCurriculum = dto.SchoolSubjectCurriculum;
            entity.StudentsuccessAverage = dto.StudentSuccessAverage;
            entity.AverageSchoolGraduates = dto.AverageSchoolGraduates;
            entity.SchoolLocalRank = dto.SchoolLocalRank;
            entity.OwningInstitution = dto.OwningInstitution;
            entity.SchoolWebsite = dto.SchoolWebsite;
            entity.SchoolPhoneNo = dto.SchoolPhoneNo;
            entity.EmailId = dto.EmailId;
            entity.SchoolCoOrdenatorName = dto.SchoolCoordinatorName;
            entity.SchoolCoOrdenatorMobile = dto.SchoolCoordinatorMobile;
            entity.SchoolCoOrdenatorEmail = dto.SchoolCoordinatorEmail;
            entity.AcademicyearStartDate = dto.AcademicYearStartDate;
            entity.AcademicyearEndDate = dto.AcademicYearEndDate;


            // entity.IsActive = dto.IsActive;

            // entity.ApprovalStatus = dto.ApprovalStatus;   // usually updated via ngo
            // entity.ApprovedBy = dto.ApprovedBy;           // usually updated via ngo
            // entity.CreatedDate = dto.CreatedDate;         // usually not updated

            await _context.SaveChangesAsync();
            return true;
        }



        // ---------------- DELETE ----------------
        public async Task<bool> DeleteAsync(long id)
        {
            var entity = await _context.MasterSchoolLists
                .FirstOrDefaultAsync(x => x.SchoolId == id);

            if (entity == null)
                return false;

            //_context.MasterSchoolLists.Remove(entity);

            // Soft delete
            entity.IsActive = false;
            await _context.SaveChangesAsync();

            return true;
        }



        // ---------------- GET BY ID ----------------
        public async Task<MasterSchoolRequestDto?> GetByIdAsync(long id)
        {
            return await _context.MasterSchoolLists
                .AsNoTracking()
                .Where(x => x.SchoolId == id)
                .Select(x => new MasterSchoolRequestDto
                {
                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    StudentCodeFormatPrefix = x.StudentCodeFormatPrefix,
                    StudentCodeFormatSufix = x.StudentCodeFormatSufix,
                    StudentCodeFormatStartingNo = x.StudentCodeFormatStartingNo,
                    StudentCodeFormatLastSavedNumber = x.StudentCodeFormatLastSavedNumber,
                    CountryId = x.CountryId,
                    CountryName = x.Country != null ? x.Country.CountryName : null,
                    ShortName = x.ShortName,
                    Area = x.Area,
                    CenterName = x.CenterName,
                    SchoolNumber = x.SchoolNumber,
                    SchoolYearOfEstablish = x.SchoolYearOfEstablish,
                    SchoolType = x.SchoolType,
                    SchoolTeachingLanguage = x.SchoolTeachingLanguage,
                    GraduatesEnglishLessThan = x.GraduatesEnglishLessThan,
                    TotalNumberOfHighSchoolLevel = x.TotalNumberOfHighSchoolLevel,
                    AverageNumberOfStudentPerClass = x.AverageNumberOfStudentPerClass,
                    SchoolAccreditations = x.SchoolAccreditations,
                    SchoolSubjectCurriculum = x.SchoolSubjectCurriculum,
                    StudentSuccessAverage = x.StudentsuccessAverage,
                    AverageSchoolGraduates = x.AverageSchoolGraduates,
                    SchoolLocalRank = x.SchoolLocalRank,
                    OwningInstitution = x.OwningInstitution,
                    SchoolWebsite = x.SchoolWebsite,
                    SchoolPhoneNo = x.SchoolPhoneNo,
                    EmailId = x.EmailId,
                    IsActive = x.IsActive,
                    SchoolCoordinatorName = x.SchoolCoOrdenatorName,
                    SchoolCoordinatorMobile = x.SchoolCoOrdenatorMobile,
                    SchoolCoordinatorEmail = x.SchoolCoOrdenatorEmail,
                    AcademicYearStartDate = x.AcademicyearStartDate,
                    AcademicYearEndDate = x.AcademicyearEndDate,
                    CreatedDate = x.CreatedDate,

                    ApprovalStatus = x.ApprovalStatus,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedByName = x.ApprovedByNavigation != null? x.ApprovedByNavigation.LoginName : null
                })
                .FirstOrDefaultAsync();
        }



        // ---------------- GET ALL FILTER ----------------
        public async Task<PagedResultDto<MasterSchoolRequestDto>> GetByFilterAsync(MasterSchoolFilterDto filter)
        {
            var query = _context.MasterSchoolLists
                .AsNoTracking()
                .AsQueryable();

            // Country filter
            if (filter.CountryId.HasValue)
            {
                query = query.Where(x => x.CountryId == filter.CountryId.Value);
            }

            // Active status filter
            if (filter.IsActive.HasValue)
            {
                query = query.Where(x => x.IsActive == filter.IsActive.Value);
            }

            // Approval status filter
            if (filter.ApprovalStatus.HasValue)
            {
                query = query.Where(x => x.ApprovalStatus == filter.ApprovalStatus.Value);
            }

            // filter date range filter
            if (filter.AcademicYearStartFrom.HasValue || filter.AcademicYearEndTo.HasValue)
            {
                var from = filter.AcademicYearStartFrom ?? DateTime.MinValue;
                var to = filter.AcademicYearEndTo ?? DateTime.MaxValue;

                query = query.Where(x =>
                    x.AcademicyearStartDate.HasValue &&
                    x.AcademicyearEndDate.HasValue &&
                    x.AcademicyearEndDate.Value >= from &&
                    x.AcademicyearStartDate.Value <= to
                );
            }

            /* Global Search */
            if (!string.IsNullOrWhiteSpace(filter.SearchText))
            {
                var search = filter.SearchText.Trim().ToLower();

                query = query.Where(x =>
                    x.SchoolName.ToLower().Contains(search) ||
                    (x.ShortName != null && x.ShortName.ToLower().Contains(search)) ||
                    (x.Area != null && x.Area.ToLower().Contains(search)) ||
                    (x.CenterName != null && x.CenterName.ToLower().Contains(search)) ||
                    (x.SchoolWebsite != null && x.SchoolWebsite.ToLower().Contains(search)) ||
                    (x.EmailId != null && x.EmailId.ToLower().Contains(search))
                );
            }


            // ---------- Total Count (before pagination) ----------
            var totalCount = await query.CountAsync();

            // ---------- Ordering ----------
            query = query.OrderByDescending(x => x.StudentData.Count());

            // ---------- Pagination rule ----------
            if (filter.PageSize > 0)
            {
                query = query
                    .Skip((filter.PageNumber - 1) * filter.PageSize)
                    .Take(filter.PageSize);
            }

            var items = await query
                .Select(x => new MasterSchoolRequestDto
                {
                    SchoolId = x.SchoolId,
                    SchoolName = x.SchoolName,
                    StudentCodeFormatPrefix = x.StudentCodeFormatPrefix,
                    StudentCodeFormatSufix = x.StudentCodeFormatSufix,
                    StudentCodeFormatStartingNo = x.StudentCodeFormatStartingNo,
                    StudentCodeFormatLastSavedNumber = x.StudentCodeFormatLastSavedNumber,
                    CountryId = x.CountryId,
                    CountryName = x.Country != null ? x.Country.CountryName : null,
                    ShortName = x.ShortName,
                    Area = x.Area,
                    CenterName = x.CenterName,
                    SchoolNumber = x.SchoolNumber,
                    SchoolYearOfEstablish = x.SchoolYearOfEstablish,
                    SchoolType = x.SchoolType,
                    SchoolTeachingLanguage = x.SchoolTeachingLanguage,
                    GraduatesEnglishLessThan = x.GraduatesEnglishLessThan,
                    TotalNumberOfHighSchoolLevel = x.TotalNumberOfHighSchoolLevel,
                    AverageNumberOfStudentPerClass = x.AverageNumberOfStudentPerClass,
                    SchoolAccreditations = x.SchoolAccreditations,
                    SchoolSubjectCurriculum = x.SchoolSubjectCurriculum,
                    StudentSuccessAverage = x.StudentsuccessAverage,
                    AverageSchoolGraduates = x.AverageSchoolGraduates,
                    SchoolLocalRank = x.SchoolLocalRank,
                    OwningInstitution = x.OwningInstitution,
                    SchoolWebsite = x.SchoolWebsite,
                    SchoolPhoneNo = x.SchoolPhoneNo,
                    EmailId = x.EmailId,
                    IsActive = x.IsActive,
                    SchoolCoordinatorName = x.SchoolCoOrdenatorName,
                    SchoolCoordinatorMobile = x.SchoolCoOrdenatorMobile,
                    SchoolCoordinatorEmail = x.SchoolCoOrdenatorEmail,
                    AcademicYearStartDate = x.AcademicyearStartDate,
                    AcademicYearEndDate = x.AcademicyearEndDate,
                    CreatedDate = x.CreatedDate,

                    ApprovalStatus = x.ApprovalStatus,
                    ApprovedBy = x.ApprovedBy,
                    ApprovedByName = x.ApprovedByNavigation != null ? x.ApprovedByNavigation.LoginName : null,

                    TotalStudents = x.StudentData != null ? x.StudentData.Count : 0
                })
                .ToListAsync();

            return new PagedResultDto<MasterSchoolRequestDto>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = filter.PageNumber,
                PageSize = filter.PageSize
            };
        }




    }
}
