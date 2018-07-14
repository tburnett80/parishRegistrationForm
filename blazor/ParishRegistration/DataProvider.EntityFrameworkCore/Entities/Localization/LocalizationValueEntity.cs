using System;

namespace DataProvider.EntityFrameworkCore.Entities.Localization
{
    public class LocalizationValueEntity
    {
        public int Id { get; set; }

        public int KeyCultureId { get; set; }

        public int TranslationCultureId { get; set; }

        public string KeyText { get; set; }

        public string TranslationText { get; set; }

        public DateTimeOffset? Created { get; set; }

        public DateTimeOffset? LastModified { get; set; }

        public virtual CultureEntity KeyCulture { get; set; }

        public virtual CultureEntity TranslationCulture { get; set; }
    }
}
