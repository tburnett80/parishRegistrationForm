﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Accessors
{
    public sealed class CacheAccessor : ICacheAccessor
    {
        #region Constructor and Private members
        private const string StateKeyPart = "CommonStates";
        private readonly ICacheProvider _provider;
        private readonly ConfigSettingsDto _settings;

        public CacheAccessor(ICacheProvider provider, ConfigSettingsDto settings)
        {
            _provider = provider
                ?? throw new ArgumentNullException(nameof(provider));

            _settings = settings
                ?? throw new ArgumentNullException(nameof(settings));
        }
        #endregion

        public IEnumerable<StateDto> GetStates()
        {
            return (IEnumerable<StateDto>) _provider.GetObjectFromCache<List<StateDto>>(StateKeyPart) ?? new StateDto[0];
        }

        public async Task CacheStates(IEnumerable<StateDto> states)
        {
            await _provider.CacheObject(StateKeyPart, states.ToList(), _settings.StateCacheTtlSeconds);
        }
    }
}
