namespace ScholarshipManagement.DTOs.SuperAdmin.Label
{
    public class LanguageLabelsDto
    {
        public string Language { get; set; } = "en";

        public bool Rtl { get; set; } = false;

        public int Version { get; set; }

        public Dictionary<string, string> Translations { get; set; } = new Dictionary<string, string>();
   
    }
}
