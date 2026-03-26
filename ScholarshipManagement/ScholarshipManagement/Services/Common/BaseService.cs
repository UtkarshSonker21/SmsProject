using ScholarshipManagement.DTOs.Common.Response;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;

namespace ScholarshipManagement.Services.Common
{
    public abstract class BaseService
    {
        protected readonly HttpClient _http;

        protected BaseService(IHttpClientFactory factory)
        {
            _http = factory.CreateClient("ApiClient");
        }

        protected static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };


        /* CORE RESPONSE HANDLER */

        protected async Task<ApiResponseDto> HandleResponse(HttpResponseMessage response)
        {
            try
            {
                return await response.Content.ReadFromJsonAsync<ApiResponseDto>()
                       ?? new ApiResponseDto 
                       { 
                           Success = false, 
                           Message = "Empty response",
                           Result = null 
                       };
            }
            catch
            {
                return new ApiResponseDto
                {
                    Success = false,
                    Message = $"Unexpected error ({(int)response.StatusCode})"
                };
            }
        }





        /* RESULT HELPERS */


        // for single object results
        protected T? GetObject<T>(ApiResponseDto response)
        {
            try
            {
                if (!response.Success || response.Result == null)
                    return default;

                if (response.Result is JsonElement json)
                    return json.Deserialize<T>(JsonOptions);

                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetObject<{typeof(T).Name}> error: {ex}");
                return default;
            }
        }


        // for list results
        protected List<T> GetList<T>(ApiResponseDto response)
        {
            try
            {
                if (!response.Success || response.Result == null)
                    return new();

                if (response.Result is JsonElement json)
                    return json.Deserialize<List<T>>(JsonOptions) ?? new();

                return new();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetList<{typeof(T).Name}> error: {ex}");
                return new();
            }
        }


        // for paged results - usually from GetAll with pagination
        protected PagedResultDto<T> GetPagedResult<T>(ApiResponseDto response)
        {
            try
            {
                if (!response.Success || response.Result == null)
                    return new PagedResultDto<T>();

                if (response.Result is JsonElement json)
                {
                    return json.Deserialize<PagedResultDto<T>>(JsonOptions)
                           ?? new PagedResultDto<T>();
                }

                return new PagedResultDto<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"GetPagedResult<{typeof(T).Name}> error: {ex}");
                return new PagedResultDto<T>();
            }
        }



    }

}
