using ScholarshipManagement.DTOs.Common.Auth;
using ScholarshipManagementAPI.DTOs.Common.Settings;

namespace ScholarshipManagement.Services.Common
{
    public class BaseCurrencyService
    {
        private BaseCurrencyDto _baseCurrency = new();

        public void SetBaseCurrency(BaseCurrencyDto? baseCurrency)
        {
            _baseCurrency = baseCurrency ?? new BaseCurrencyDto ();
        }

        public BaseCurrencyDto GetBaseCurrency()
        {
            return _baseCurrency;
        }
    }
}
