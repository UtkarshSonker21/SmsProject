using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.UsersLogin;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersLoginService : BaseService
    {
        //private readonly HttpClient _http;

        public UsersLoginService(IHttpClientFactory factory) : base(factory) { }



        public async Task<(PagedResultDto<UsersLoginRequestDto> Data, ApiResponseDto Response)> GetUsersLogin(UsersLoginFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersLoginRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }


        public async Task<(UsersLoginRequestDto? Data, ApiResponseDto)> GetUsersLoginById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-login/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersLoginRequestDto>(apiResponse);

            return (data, apiResponse);
        }


    }
}
