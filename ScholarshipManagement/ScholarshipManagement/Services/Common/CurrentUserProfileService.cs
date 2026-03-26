using ScholarshipManagement.DTOs.Common.Auth;
using ScholarshipManagement.DTOs.Common.Menu;
using ScholarshipManagement.DTOs.Common.Settings;

namespace ScholarshipManagement.Services.Common
{
    public class CurrentUserProfileService
    {
        private CurrentUserProfileDto _currentUser = new();

        public void SetCurrentUserProfile(CurrentUserProfileDto? user)
        {
            _currentUser = user ?? new CurrentUserProfileDto();
        }

        public CurrentUserProfileDto GetCurrentUserProfile()
        {
            return _currentUser;
        }


        // will implemnt this later

        //public string LoginName => _currentUser.LoginName ?? "";
        //public long LoginId => _currentUser.LoginId;
        //public List<AvailableRolesDto> Roles => _currentUser.AvailableRoles ?? new();


        //loginName = CurrentUserProfileService.LoginName;
        //loginId = CurrentUserProfileService.LoginId;
        //availableRoles = CurrentUserProfileService.Roles;

    }
}
