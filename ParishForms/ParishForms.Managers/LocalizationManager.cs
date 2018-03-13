using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Managers
{
    public sealed class LocalizationManager : ILocalizationManager
    {
        #region Constructor and Private members
        private readonly ILocalizationEngine _engine;

        public LocalizationManager(ILocalizationEngine engine)
        {
            _engine = engine
                ?? throw new ArgumentNullException(nameof(engine));
        }
        #endregion

        public async Task PreLoadCache()
        {
            await _engine.PreLoadCache();
        }

        public async Task<IEnumerable<StateDto>> GetStates()
        {
            return await _engine.GetStates();
        }

        public async Task<IEnumerable<TranslationDto>> GetTranslations(string culture)
        {
            return await _engine.GetTranslationsForCulture(culture);
        }

        public async Task<IEnumerable<CultureDto>> GetCultureList()
        {
            return await _engine.GetCultureList();
        }
    }
}
