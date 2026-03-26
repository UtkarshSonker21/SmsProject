using System.Reflection;
namespace ScholarshipManagement.Helper
{
    public static class AppVersion
    {
        public static string Current
        {
            get
            {
                var v = typeof(Program).Assembly.GetName().Version;
                if (v == null) return "unknown";
                return $"{v.Major}.{v.Minor}.{v.Build}";
            }
        }
    }
}
