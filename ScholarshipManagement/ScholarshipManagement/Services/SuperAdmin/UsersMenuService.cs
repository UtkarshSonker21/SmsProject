using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.SuperAdmin.GeneralSettings;
using ScholarshipManagement.DTOs.SuperAdmin.UsersMenu;
using ScholarshipManagement.Services.Common;
using System.Net.Http.Json;

namespace ScholarshipManagement.Services.SuperAdmin
{
    public class UsersMenuService:BaseService
    {
        //private readonly HttpClient _http;

        public UsersMenuService(IHttpClientFactory factory) : base(factory) { }


        public async Task<(PagedResultDto<UsersMenuRequestdto> Data, ApiResponseDto Response)> GetUsersMenu(UsersMenuFilterDto filter)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-menu/search", filter);

            var apiResponse = await HandleResponse(response);

            var data = GetPagedResult<UsersMenuRequestdto>(apiResponse);
            apiResponse.Result = data;

            return (data, apiResponse);
        }



        public async Task<(UsersMenuRequestdto? Data, ApiResponseDto)> GetUsersMenuById(long id)
        {
            var response = await _http.GetAsync(
                $"superadmin/users-menu/getById/{id}");

            var apiResponse = await HandleResponse(response);

            var data = GetObject<UsersMenuRequestdto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<ApiResponseDto> AddUsersMenu(UsersMenuRequestdto dto)
        {
            var response = await _http.PostAsJsonAsync(
                "superadmin/users-menu/create", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> UpdateUsersMenu(UsersMenuRequestdto dto)
        {
            var response = await _http.PutAsJsonAsync(
                $"superadmin/users-menu/update/{dto.MenuLinkId}", dto);

            return await HandleResponse(response);
        }


        public async Task<ApiResponseDto> DeleteUsersMenu(long id)
        {
            var response = await _http.DeleteAsync(
                $"superadmin/users-menu/delete/{id}");

            return await HandleResponse(response);
        }


    }
}
