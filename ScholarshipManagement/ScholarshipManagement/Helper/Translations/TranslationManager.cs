using MudBlazor;
using ScholarshipManagement.DTOs.Common.Settings;
using ScholarshipManagement.DTOs.SuperAdmin.Label;
using ScholarshipManagement.Helper.Enums;
using ScholarshipManagement.Services.SuperAdmin;
using System.Net;

namespace ScholarshipManagement.Helper.Translations
{
    public class TranslationManager
    {
        private readonly LabelService _api;
        private readonly TranslationStorageService _storage;
        private readonly TranslationStateService _translationState;


        //private Dictionary<string, string> _translations = new();

        //public bool IsRtl { get; private set; }

        public TranslationManager(
            LabelService api,
            TranslationStorageService storage,
            TranslationStateService translationState)
        {
            _api = api;
            _storage = storage;
            _translationState = translationState;
        }

        public async Task<bool> Load(LanguageCode language)
        {
            var lang = language.ToString().ToLower();

            // Check cache
            var cache = await _storage.Get(lang);

            // Call API
            var result = await _api.GetTranslations(language);

            if (!result.Response.Success)
                return false;

            var data = result.Data;

            // Version check
            if (cache != null && cache.Version == data.Version)
            {
                _translationState.Set(new LanguageLabelsDto
                {
                    Language = cache.Language,
                    Version = cache.Version,
                    Rtl = cache.Rtl,
                    Translations = cache.Translations
                });

                return true;
            }

            // Clear old cache (only 1 language)
            await ClearAllCache();

            // Store new cache
            var newCache = new TranslationCache
            {
                Language = data.Language,
                Version = data.Version,
                Rtl = data.Rtl,
                Translations = data.Translations
            };

            await _storage.Set(newCache);

            // Set GLOBAL state (IMPORTANT)
            _translationState.Set(data);

            return true;
        }


        private async Task ClearAllCache()
        {
            foreach (var lang in Enum.GetValues<LanguageCode>())
            {
                var langCode = lang.ToString().ToLower();
                await _storage.Remove(langCode);
            }
        }






        //public string T(string key)
        //{
        //    if (string.IsNullOrWhiteSpace(key))
        //        return string.Empty;


        //    if (_translations.TryGetValue(key, out var value))
        //        return value;

        //    return key;

        //}


    }
}
