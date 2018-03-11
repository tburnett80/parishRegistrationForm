using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Models.Common;

namespace ParishForms.Engines
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public sealed class LocalizationEngine : ILocalizationEngine
    {
        #region Constructor and Private members
        private readonly ILocalizationAccessor _localizationAccessor;
        private readonly ICacheAccessor _cacheAccessor;

        public LocalizationEngine(ILocalizationAccessor localizationAccessor, ICacheAccessor cacheAccessor)
        {
            _localizationAccessor = localizationAccessor
                ?? throw new ArgumentNullException(nameof(localizationAccessor));

            _cacheAccessor = cacheAccessor
                ?? throw new ArgumentNullException(nameof(cacheAccessor));
        }
        #endregion

        public async Task<IEnumerable<StateDto>> GetStates()
        {
            var cached = _cacheAccessor.GetStates();
            if (cached.Any())
                return cached;

            var states = await _localizationAccessor.GetStates();
            await _cacheAccessor.CacheStates(states);

            return states;
        }
    }
}
