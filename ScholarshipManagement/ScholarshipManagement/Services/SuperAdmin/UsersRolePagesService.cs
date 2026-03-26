using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.UsersLoginRole;
using ScholarshipManagement.DTOs.SuperAdmin.UsersRolePage;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersRolePagesService : BaseService
    {
        //private readonly HttpClient _http;

        public UsersRolePagesService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<UsersRolePageRequestDto> Data, ApiResponseDto Response)> GetUsersRolePages(UsersRolePageFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role-pages/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersRolePageRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UsersRolePageRequestDto? Data, ApiResponseDto)> GetUsersRolePagesById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-role-pages/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersRolePageRequestDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddUsersRolePage(UsersRolePageRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role-pages/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateUsersRolePage(UsersRolePageRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/users-role-pages/update/{dto.RoleId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteUsersRolePage(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/users-role-pages/delete/{id}");

            return await HandleResponse(response);
        }



        public async Task<(PagedResultDto<RolePagePermissionDto> Data, ApiResponseDto Response)> GetRolePermissions(UsersRolePageFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role-pages/role-permissions", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<RolePagePermissionDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> BulkSaveRolePermissionsAsync(RolePermissionBulkSaveDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role-pages/role-permissions/bulk-save", dto);

            return await HandleResponse(response);
        }


    }
}
