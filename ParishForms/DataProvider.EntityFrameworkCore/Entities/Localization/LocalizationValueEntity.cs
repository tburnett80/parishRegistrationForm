using System;

namespace DataProvider.EntityFrameworkCore.Entities.Localization
{
    public class LocalizationValueEntity
    {
        public int Id { get; set; }

        public string KeyText { get; set; }

        public string KeyCulture { get; set; }

        public string TranslationText { get; set; }

        public string TranslationCulture { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastModified { get; set; }
    }
}
