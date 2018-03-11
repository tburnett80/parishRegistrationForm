
namespace ParishForms.Common.Models
{
    public sealed class ConfigSettingsDto
    {
        public string ConnectionString { get; set; }

        public int StateCacheTtlSeconds { get; set; }
    }
}
