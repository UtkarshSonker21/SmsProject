using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.UsersLoginLog;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersLoginLogService :BaseService
    {
        public UsersLoginLogService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<UsersLoginLogRequestDto> Data, ApiResponseDto Response)> GetUsersLoginLog(UsersLoginLogFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-login-log/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersLoginLogRequestDto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



        public async Task<(UsersLoginLogRequestDto? Data, ApiResponseDto)> GetUsersLoginLogById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-login-log/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersLoginLogRequestDto>(apiResponse);

            return (data, apiResponse);
        }

    }
}
