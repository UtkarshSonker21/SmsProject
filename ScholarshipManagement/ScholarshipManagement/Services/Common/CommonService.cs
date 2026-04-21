using ScholarshipManagement.DTOs.Common.HrStaff;
using ScholarshipManagement.DTOs.Common.Menu;
using ScholarshipManagement.DTOs.Common.Response;
using ScholarshipManagement.DTOs.Common.Settings;
using ScholarshipManagement.DTOs.SuperAdmin.MasterDropdown;
using System.Net.Http.Json;
using static ScholarshipManagement.Helper.Constant;

namespace ScholarshipManagement.Services.Common
{
    public class CommonService:BaseService
    {
        public CommonService(IHttpClientFactory factory) : base(factory) { }

        public async Task<(List<UsersModuleDto> Data, ApiResponseDto Response)> GetAllUsersModule()
        {
            var response = await _http.GetAsync(ApiEndpoints.GetAllUsersModule);

            var apiResponse = await HandleResponse(response);

            var data = GetList<UsersModuleDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<(List<LoadMenuDto> Data, ApiResponseDto Response)> GetAllMenu()
        {
            var response = await _http.GetAsync(ApiEndpoints.GetAllMenu);

            var apiResponse = await HandleResponse(response);

            var data = GetList<LoadMenuDto>(apiResponse);

            return (data, apiResponse);
        }


        public async Task<(DashboardDto Data, ApiResponseDto Response)> GetDashboard()
        {
            var response = await _http.GetAsync(ApiEndpoints.GetDashboard);

            var apiResponse = await HandleResponse(response);

            var data = GetObject<DashboardDto>(apiResponse) ?? new DashboardDto();

            return (data, apiResponse);
        }



    }
}
