using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.UsersLoginRole;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersLoginRoleService :BaseService
    {
        //private readonly HttpClient _http;

        public UsersLoginRoleService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<UsersLoginRoleRequestDto> Data, ApiResponseDto Response)> GetUsersLoginRole(UsersLoginRoleFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login-role/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersLoginRoleRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UsersLoginRoleRequestDto? Data, ApiResponseDto Response)> GetUsersLoginRoleById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-login-role/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersLoginRoleRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddUsersLoginRole(UsersLoginRoleRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login-role/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateUsersLoginRole(UsersLoginRoleRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/users-login-role/update/{dto.UserLoginRoleId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteUsersLoginRole(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/users-login-role/delete/{id}");

            return await HandleResponse(response);
        }



        public async Task<(PagedResultDto<LoginRoleAssignmentDto> Data, ApiResponseDto Response)> GetRolesByLogin(UsersLoginRoleFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login-role/login-roles", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<LoginRoleAssignmentDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> BulkSaveRolesAsync(LoginRoleBulkSaveDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login-role/bulk-save", dto);

            return await HandleResponse(response);
        }


    }
}
