using ScholarshipManagement.DTOs.Common.Settings;
using ScholarshipManagement.Helper;
using System.Text.Json;
using static ScholarshipManagement.Helper.Constant;

namespace ScholarshipManagement.Helper.Translations
{
    public class TranslationStorageService
    {
        private readonly LocalStorageService _localStorage;
        private const string KeyPrefix = LocalStorageKeys.Translations;

        public TranslationStorageService(LocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<TranslationCache?> Get(string lang)
        {
            var json = await _localStorage.GetItem(KeyPrefix + lang);

            if (string.IsNullOrWhiteSpace(json))
                return null;

            return JsonSerializer.Deserialize<TranslationCache>(json);
        }

        public async Task Set(TranslationCache cache)
        {
            var json = JsonSerializer.Serialize(cache);

            await _localStorage.SetItem(KeyPrefix + cache.Language, json);
        }

        public async Task Remove(string lang)
        {
            await _localStorage.RemoveItem(KeyPrefix + lang);
        }
    }
}
