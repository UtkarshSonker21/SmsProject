namespace ScholarshipManagement.DTOs.Common.Settings
{
    public class TranslationCache
    {
        public string Language { get; set; } = string.Empty;

        public int Version { get; set; }

        public bool Rtl { get; set; }

        public Dictionary<string, string> Translations { get; set; } = new();
    }
}
