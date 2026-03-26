using ScholarshipManagement.DTOs.SuperAdmin.Label;

namespace ScholarshipManagement.Helper.Translations
{
    public class TranslationStateService
    {
        private LanguageLabelsDto _current = new();
        public string CurrentLanguage => _current.Language ?? "en";


        public event Action? OnChange;

        public void Set(LanguageLabelsDto? data)
        {
            if (data == null)
                return;

            if (_current.Language == data.Language && _current.Version == data.Version)
                return;

            _current = data;
            Notify();
        }

        private void Notify() => OnChange?.Invoke();

        public LanguageLabelsDto Get() => _current;

        public string T(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                return string.Empty;

            if (_current.Translations != null &&
                _current.Translations.TryGetValue(key, out var value))
                return value;

            return key;
        }

        public bool IsRtl => _current.Rtl;
    }


}
