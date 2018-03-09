
namespace ParishForms.Common.Models
{
    public sealed class TranslationDto
    {
        public string KeyText { get; set; }

        public string KeyCulture { get; set; }

        public string LocalizedCulture { get; set; }

        public string LocalizedText { get; set; }
    }
}
