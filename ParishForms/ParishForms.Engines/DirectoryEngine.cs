using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Extensions;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Engines
{
    [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
    public sealed class DirectoryEngine : IDirectoryEngine, IDirectoryExportEngine
    {
        #region Constructor and Private members
        private readonly IDirectoryAccessor _directoryAccessor;
        private readonly ICacheAccessor _cacheAccessor;
        private readonly IExportAccessor _exportAccessor;

        public DirectoryEngine(IDirectoryAccessor directoryAccessor, ICacheAccessor cacheAccessor, IExportAccessor exportAccessor)
        {
            _directoryAccessor = directoryAccessor
                ?? throw new ArgumentNullException(nameof(directoryAccessor));

            _cacheAccessor = cacheAccessor
                ?? throw new ArgumentNullException(nameof(cacheAccessor));

            _exportAccessor = exportAccessor
                ?? throw new ArgumentNullException(nameof(exportAccessor));
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

        public async Task<SaveResult> StoreSubmision(SubmisionDto submision)
        {
            var state = await GetStateByAbbr(submision.HomeAddress.State.Abbreviation);
            if(state == null)
                throw new InvalidDataException($"Could not match state: {submision.HomeAddress.State.Abbreviation}");

            submision.HomeAddress.State.Id = state.Id;

            var rowCount = await _directoryAccessor.StoreSubmision(submision);

            return new SaveResult
            {
                Type = DeterminExpectedRowCount(submision) == rowCount
                    ? ResultType.Success
                    : ResultType.SaveFailure,
                RowsAffected = rowCount
            };
        }

        public bool ValidateSubmision(SubmisionDto submision)
        {
            if (submision == null)
                return false;

            if (!submision.FamilyName.HasValue())
                return false;

            if (submision.HomeAddress == null)
                return false;

            if (!submision.HomeAddress.City.HasValue())
                return false;

            if (string.IsNullOrEmpty(submision.HomeAddress.Zip))
                return false;

            if (string.IsNullOrEmpty(submision.HomeAddress.Street))
                return false;

            if (!ValidatePhone(submision.HomePhone))
                return false;

            if (!ValidatePhone(submision.AdultOneMobilePhone))
                return false;

            if (!ValidatePhone(submision.AdultTwoMobilePhone))
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

        #region Export Impl
        public async Task<ExportRequestDto> QueueRequest(int userId, string email)
        {
            var req = CreateNewExportRequest(userId, email);
            return await _exportAccessor.QueueRequest(req);
        }
        #endregion

        #region Private methods
        private int DeterminExpectedRowCount(SubmisionDto dto)
        {
            var count = 1;
            if (dto.HomeAddress != null)
                count++;

            if (dto.HomePhone != null)
                count++;

            if (dto.AdultOneEmailAddress != null)
                count++;

            if (dto.AdultOneMobilePhone != null)
                count++;

            if (dto.AdultTwoEmailAddress != null)
                count++;

            if (dto.AdultTwoMobilePhone != null)
                count++;

            return count;
        }

        private bool ValidatePhone(PhoneDto dto, bool isRequired = false)
        {
            if (isRequired && dto == null)
                return false;

            if (isRequired)
                return dto.Number.IsNumeric();
            
            if (dto == null)
                return true;

            // ReSharper disable once SimplifyConditionalTernaryExpression
            return dto.Number.HasValue() 
                ? dto.Number.IsNumeric()
                : true;
        }

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

        private ExportRequestDto CreateNewExportRequest(int userId, string email)
        {
            return new ExportRequestDto
            {
                Email = email,
                ExportType = ExportRequestType.Directory,
                RequestId = Guid.NewGuid(),
                StartRange = 1,
                Status = ExportStatus.InQueue,
                UserId = userId
            };
        }
        #endregion
    }
}
