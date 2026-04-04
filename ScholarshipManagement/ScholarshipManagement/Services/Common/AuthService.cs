using ScholarshipManagement.DTOs.Common.Auth;
using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.Common.Settings;
using ScholarshipManagement.Helper;
using ScholarshipManagement.Helper.Enums;
using System.Globalization;
using System.Net.Http.Json;
using System.Text.Json;
using static ScholarshipManagement.Helper.Constant;
using static System.Net.WebRequestMethods;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace ScholarshipManagement.Services.Common
{
    public class AuthService : BaseService
    {
        private readonly LocalStorageService _localStorage;
        public AuthService(IHttpClientFactory factory, LocalStorageService localStorage) : base(factory)
        {
            _localStorage = localStorage;
        }


        public async Task<ApiResponseDto> Login(LoginRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthLogin, dto);

            return await ProcessLoginResponse(response, dto.RememberMe);

        }


        public async Task<ApiResponseDto> Logout()
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthLogout,"");

            var apiResponse = await HandleResponse(response);

            if (apiResponse.Success)
            {
                await ClearUserSession();
            }

            return apiResponse;
        }


        public async Task<ApiResponseDto> LoginWithCode(UserIdentifierDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthLoginWithCode, request);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> VerifyLoginCode(VerifyOtpDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthVerifyLoginCode, request);

            return await ProcessLoginResponse(response, rememberMe: true);
        }


        public async Task<ApiResponseDto> SwitchRole(long roleId)
        {
            var response = await _http.PostAsync(
                $"{ApiEndpoints.AuthSwitchRole}?roleId={roleId}",
                null);

            var apiResponse = await HandleResponse(response);

            if (!apiResponse.Success)
                return apiResponse;

            var data = GetObject<LoginResponseDto>(apiResponse);

            if (data == null || string.IsNullOrWhiteSpace(data.Token))
            {
                return new ApiResponseDto
                {
                    Success = false,
                    Message = "Invalid switch role response"
                };
            }

            // Important: always use session storage (no rememberMe here)
            // Role switch is temporary (session-based), not permanent
            // prevents saving non-default role across browser sessions
            // so app resets to default role on next login

            await StoreUserSession(data, false);

            return apiResponse;
        }


        public async Task<ApiResponseDto> ForgotUserName(UserIdentifierDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthForgotUserName, request);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> ForgotUserPassword(UserIdentifierDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthForgotPassword, request);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> ResetPassword(ResetPasswordRequestDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthResetPassword, request);

            return await HandleResponse(response);
        }

        public async Task<ApiResponseDto> ResetUserName(ResetUserNameRequestDto request)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthResetUsername, request);

            return await HandleResponse(response);
        }

        public async Task<(CurrentUserProfileDto Data, ApiResponseDto Response)> GetMyProfile()
        {
            var response = await _http.GetAsync(ApiEndpoints.AuthMyProfile);

            var apiResponse = await HandleResponse(response);

            var data = GetObject<CurrentUserProfileDto>(apiResponse) ?? new CurrentUserProfileDto();

            return (data, apiResponse);
        }

        public async Task<ApiResponseDto> UpdateMyProfile(UpdateMyProfileDto dto)
        {
            var response = await _http.PostAsJsonAsync(ApiEndpoints.AuthUpdateMyProfile, dto);

            return await HandleResponse(response);
        }



        #region Private Methods

        private async Task<ApiResponseDto> ProcessLoginResponse(HttpResponseMessage response, bool rememberMe)
        {
            var apiResponse = await HandleResponse(response);

            if (!apiResponse.Success)
                return apiResponse;

            var data = GetObject<LoginResponseDto>(apiResponse);

            if (data == null || string.IsNullOrWhiteSpace(data.Token))
            {
                return new ApiResponseDto
                {
                    Success = false,
                    Message = "Invalid login response from server"
                };
            }

            await StoreUserSession(data, rememberMe); // ✅ reuse

            return apiResponse;
        }


        private async Task StoreUserSession(LoginResponseDto data, bool rememberMe)
        {
            // Token
            if (rememberMe)
                await _localStorage.SetTokenPersistent(data.Token);
            else
                await _localStorage.SetTokenSession(data.Token);

            // Expiry
            await _localStorage.SetItem(LocalStorageKeys.TokenExpiry, data.Expiry.ToString("O"));

            // User
            await _localStorage.SetItem(LocalStorageKeys.LoginId, data.LoginId.ToString());
            await _localStorage.SetItem(LocalStorageKeys.LoginName, data.LoginName);

            // Module
            await _localStorage.SetItem(LocalStorageKeys.ModuleId, data.ModuleId.ToString());
            await _localStorage.SetItem(LocalStorageKeys.ModuleName, data.ModuleName);
            await _localStorage.SetItem(LocalStorageKeys.StaffType, data.ModuleName);

            // Role
            await _localStorage.SetItem(LocalStorageKeys.CurrentRoleId, data.CurrentRoleId.ToString());
            await _localStorage.SetItem(LocalStorageKeys.CurrentRoleName, data.CurrentRoleName);

            // Roles list
            await _localStorage.SetItem(
                LocalStorageKeys.AvailableRoles,
                JsonSerializer.Serialize(data.AvailableRoles ?? new List<AvailableRolesDto>())
            );
        }


        private async Task ClearUserSession()
        {
            // Remove token from both storages (safety)
            await _localStorage.RemoveToken();

            // Expiry
            await _localStorage.RemoveItem(LocalStorageKeys.TokenExpiry);

            // User
            await _localStorage.RemoveItem(LocalStorageKeys.LoginId);
            await _localStorage.RemoveItem(LocalStorageKeys.LoginName);

            // Module
            await _localStorage.RemoveItem(LocalStorageKeys.ModuleId);
            await _localStorage.RemoveItem(LocalStorageKeys.ModuleName);
            await _localStorage.RemoveItem(LocalStorageKeys.StaffType);

            // Role
            await _localStorage.RemoveItem(LocalStorageKeys.CurrentRoleId);
            await _localStorage.RemoveItem(LocalStorageKeys.CurrentRoleName);

            // Roles list
            await _localStorage.RemoveItem(LocalStorageKeys.AvailableRoles);
        }


        #endregion



        #region Helper Methods



        public async Task<bool> IsUserLoggedIn()
        {
            var token = await _localStorage.GetToken();
            if (string.IsNullOrWhiteSpace(token))
                return false;

            return !await IsTokenExpired();
        }

        public async Task<bool> IsTokenExpired()
        {
            var expiryStr = await _localStorage.GetItem(LocalStorageKeys.TokenExpiry);

            if (string.IsNullOrWhiteSpace(expiryStr))
                return true;

            var expiry = DateTime.Parse(expiryStr, null, DateTimeStyles.RoundtripKind);

            return DateTime.UtcNow >= expiry;
        }

        public async Task<string?> GetToken()
        {
            return await _localStorage.GetToken();

        }


        public async Task<List<AvailableRolesDto>> GetAvailableRolesAsync()
        {
            try
            {
                var rolesJson = await _localStorage.GetItem(LocalStorageKeys.AvailableRoles);

                if (string.IsNullOrWhiteSpace(rolesJson))
                    return new List<AvailableRolesDto>();

                return JsonSerializer.Deserialize<List<AvailableRolesDto>>(rolesJson)
                       ?? new List<AvailableRolesDto>();
            }
            catch
            {
                // Optional: log error
                return new List<AvailableRolesDto>();
            }
        }


        #endregion

    }
}






//public async Task<long?> GetLoginId()
//{
//    var value = await _localStorage.GetItem(LocalStorageKeys.LoginId);
//    return long.TryParse(value, out var id) ? id : null;
//}

//public async Task<string?> GetLoginName()
//{
//    return await _localStorage.GetItem(LocalStorageKeys.LoginName);
//}

//public async Task<long?> GetCurrentRoleId()
//{
//    var value = await _localStorage.GetItem(LocalStorageKeys.CurrentRoleId);
//    return long.TryParse(value, out var id) ? id : null;
//}

//public async Task<long?> GetModuleId()
//{
//    var value = await _localStorage.GetItem(LocalStorageKeys.ModuleId);
//    return long.TryParse(value, out var id) ? id : null;
//}

//public async Task<StaffType?> GetStaffType()
//{
//    var value = await _localStorage.GetItem(LocalStorageKeys.StaffType);

//    if (Enum.TryParse<StaffType>(value, out var type))
//        return type;

//    return null;
//}
