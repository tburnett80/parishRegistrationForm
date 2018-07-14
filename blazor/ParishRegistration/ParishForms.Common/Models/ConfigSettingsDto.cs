
namespace ParishForms.Common.Models
{
    public sealed class ConfigSettingsDto
    {
        public string ConnectionString { get; set; }

        public int StateCacheTtlSeconds { get; set; }

        public int TranslationCacheTtlSeconds { get; set; }

        public string RedirectUrl { get; set; }

        public string RelayAddress { get; set; }
    }
}
