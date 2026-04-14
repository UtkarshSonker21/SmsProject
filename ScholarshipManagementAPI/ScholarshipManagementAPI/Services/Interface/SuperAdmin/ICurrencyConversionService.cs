using ScholarshipManagementAPI.DTOs.Common.Response;
using ScholarshipManagementAPI.DTOs.SuperAdmin.CurrencyConversion;

namespace ScholarshipManagementAPI.Services.Interface.SuperAdmin
{
    public interface ICurrencyConversionService
    {
        Task<CurrencySyncResultDto> SyncRatesAsync(string triggeredBy);

        Task<Dictionary<string, decimal>> GetLatestRates(string baseCurrency);




        Task<long> AddManualRate(CurrencyConversionRequestDto dto);
        Task<CurrencyConversionRequestDto?> GetByIdAsync(long id);
        Task<List<CurrencyConversionRequestDto>> GetCurrentCurrencyRateAsync();
        Task<PagedResultDto<CurrencyConversionRequestDto>> GetByFilterAsync(CurrencyConversionFilterDto filter);

    }
}
