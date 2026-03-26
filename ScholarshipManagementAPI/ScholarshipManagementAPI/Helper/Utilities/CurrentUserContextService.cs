using Microsoft.EntityFrameworkCore;
using ScholarshipManagementAPI.Data.Contexts;
using ScholarshipManagementAPI.DTOs.Common.Auth;
using ScholarshipManagementAPI.Helper.Enums;

namespace ScholarshipManagementAPI.Helper.Utilities
{
    public class CurrentUserContextService
    {
        private readonly AppDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserContextService(
            AppDbContext context,
            IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoggedInUserDto> GetCurrentUserAsync()
        {
            var user = _httpContextAccessor.HttpContext!.User;

            // Read JWT claims (fast)
            var loginId = JwtClaimHelper.LoginId(user);
            var roleId = JwtClaimHelper.RoleId(user);
            var moduleId = JwtClaimHelper.ModuleId(user);

            if (!Enum.IsDefined(typeof(StaffType), (int)moduleId))
                throw new UnauthorizedAccessException("Invalid module");

            // Fetch tenant mapping ONCE (DB)
            var staffInfo = await _context.UsersLogins
                .AsNoTracking()
                .Where(x => x.LoginId == loginId)
                .Select(x => new
                {
                    x.StaffId,
                    x.Staff.StaffType,
                    x.Staff.UniversityId,
                    x.Staff.SchoolId,
                    x.Staff.NgoId
                })
                .FirstOrDefaultAsync();

            if (staffInfo == null)
                throw new UnauthorizedAccessException("Staff mapping not found");

            // Build context
            return new LoggedInUserDto
            {
                LoginId = loginId,
                RoleId = roleId,
                ModuleId = moduleId,

                RoleName = JwtClaimHelper.RoleName(user),
                UserName = JwtClaimHelper.UserName(user),

                StaffType = (StaffType)moduleId,

                StaffId = staffInfo.StaffId,
                UniversityId = staffInfo.UniversityId,
                SchoolId = staffInfo.SchoolId,
                NgoId = staffInfo.NgoId
            };
        }              
    }
}
