using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Logging;

namespace ParishForms.Managers
{
    public class DirectoryManager : IDirectoryManager
    {
        #region Constructor and Private Members
        private readonly IDirectoryEngine _directoryEngine;
        private readonly ILogAccessor _logger;

        public DirectoryManager(IDirectoryEngine directoryEngine, ILogAccessor logger)
        {
            _directoryEngine = directoryEngine
                ?? throw new ArgumentNullException(nameof(directoryEngine));

            _logger = logger
                ?? throw new ArgumentNullException(nameof(logger));
        }
        #endregion

        #region Contract Impl
        public async Task<IEnumerable<StateDto>> GetStateList()
        {
            return await _directoryEngine.GetStates();
        }

        public async Task<SaveResult> StoreSubmision(SubmisionDto submision)
        {
            try
            {
                if (!_directoryEngine.ValidateSubmision(submision))
                    return new SaveResult { Type = ResultType.ValidationFailed };

                return await _directoryEngine.StoreSubmision(submision);
            }
            catch (Exception ex)
            {
                await _logger.LogException(new ExceptionLogDto(ex));
                return new SaveResult
                {
                    Type = ResultType.Exception,
                    Message = ex.Message
                };
            }
        }

        public async Task<IDictionary<string, int>> GetFormLimits()
        {
            return await _directoryEngine.GetFormLimits();
        }

        #endregion
    }
}
