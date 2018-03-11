using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Accessors
{
    internal static class EntityToDtoMapper
    {
        internal static StateDto ToDto(this StateEntity ent)
        {
            if (ent == null)
                return null;

            return new StateDto
            {
                Id = ent.Id,
                Abbreviation = ent.Abbreviation,
                Name = ent.Name
            };
        }

        internal static TranslationDto ToDto(this LocalizationValueEntity ent)
        {
            if (ent == null)
                return null;

            return new TranslationDto
            {
                KeyCulture = ent.KeyCulture,
                KeyText = ent.KeyText,
                LocalizedCulture = ent.TranslationCulture,
                LocalizedText = ent.TranslationText
            };
        }
    }
}
