using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.UsersRole;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersRoleService : BaseService
    {
        //private readonly HttpClient _http;

        public UsersRoleService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<UsersRoleRequestDto> Data, ApiResponseDto Response)> GetUsersRole(UsersRoleFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersRoleRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UsersRoleRequestDto? Data, ApiResponseDto)> GetUsersRoleById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-role/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersRoleRequestDto>(apiResponse);

            return (data, apiResponse);
        }



        public async Task<ApiResponseDto> AddUsersRole(UsersRoleRequestDto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-role/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateUsersRole(UsersRoleRequestDto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/users-role/update/{dto.RoleId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteUsersRole(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/users-role/delete/{id}");

            return await HandleResponse(response);
        }


    }
}
