using DataProvider.EntityFrameworkCore.Entities.Common;
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
    }
}
