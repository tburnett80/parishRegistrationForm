using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Extensions;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Engines
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public sealed class DirectoryEngine : IDirectoryEngine
    {
        #region Constructor and Private members
        private readonly IDirectoryAccessor _directoryAccessor;
        private readonly ICacheAccessor _cacheAccessor;

        public DirectoryEngine(IDirectoryAccessor directoryAccessor, ICacheAccessor cacheAccessor)
        {
            _directoryAccessor = directoryAccessor
                ?? throw new ArgumentNullException(nameof(directoryAccessor));

            _cacheAccessor = cacheAccessor
                ?? throw new ArgumentNullException(nameof(cacheAccessor));
        }
        #endregion

        #region Contract Impl
        public async Task<IEnumerable<StateDto>> GetStates()
        {
            var cached = _cacheAccessor.GetStates();
            if (cached.Any())
                return cached;

            var states = await _directoryAccessor.GetStates();
            await _cacheAccessor.CacheStates(states);

            return states;
        }

        public async Task<int> StoreSubmision(SubmisionDto submision)
        {
            //validate can save

            var state = await GetStateByAbbr(submision.HomeAddress.State.Abbreviation);
            submision.HomeAddress.State.Id = state.Id;

            return await _directoryAccessor.StoreSubmision(submision);
        }

        public bool ValidateSubmision(SubmisionDto submision)
        {
            if (string.IsNullOrEmpty(submision.FamilyName.TryTrim()))
                return false;

            return !string.IsNullOrEmpty(submision.AdultOneFirstName.TryTrim());
        }

        public async Task<IDictionary<string, int>> GetFormLimits()
        {
            var fromCache = _cacheAccessor.GetDirectoryFormLimits();
            if (fromCache.Keys.Any())
                return fromCache;

            var sublimits = await _directoryAccessor.GetFieldLengths<SubmisionDto>();
            var addressLimits = await _directoryAccessor.GetFieldLengths<AddressDto>();
            var emailLimits = await _directoryAccessor.GetFieldLengths<EmailDto>();
            var phoneLimits = await _directoryAccessor.GetFieldLengths<PhoneDto>();

            var results = Merge(emailLimits, Merge(phoneLimits, Merge(addressLimits, sublimits)));
            await _cacheAccessor.CacheDirectoryFormLimits(results);

            return results;
        }
        #endregion

        #region Private methods
        private async Task<StateDto> GetStateByAbbr(string abbr)
        {
            var states = await GetStates();
            return states.FirstOrDefault(s => 
                s.Abbreviation.ToUpper().Equals(abbr.ToUpper()));
        }

        private IDictionary<string, int> Merge(IDictionary<string, int> fist, IDictionary<string, int> second)
        {
            foreach (var key in fist.Keys)
            {
                if(second.ContainsKey(key))
                    continue;

                second.Add(key, fist[key]);
            }

            return second;
        }
        #endregion
    }
}
