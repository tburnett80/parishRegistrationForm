using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Accessors
{
    public sealed class LocalizationAccessor : ILocalizationAccessor
    {
        #region Constructor and Private Members
        private readonly IDbContextFactory<LocalizationContext> _contextFactory;

        public LocalizationAccessor(IDbContextFactory<LocalizationContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        public async Task<IEnumerable<StateDto>> GetStates()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ents = await ctx.States.ToListAsync();
                return ents.Select(e => e.ToDto());
            }
        }

        public async Task<IEnumerable<string>> GetListOfCultures()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                return await ctx.Translations
                    .Select(t => t.TranslationCulture)
                    .Distinct()
                    .ToListAsync();
            }
        }

        public async Task<IEnumerable<TranslationDto>> GetTranslations(string culture)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ents = await ctx.Translations
                    .Where(t => t.TranslationCulture.Equals(culture))
                    .ToListAsync();

                return ents.Select(e => e.ToDto());
            }
        }
    }
}
