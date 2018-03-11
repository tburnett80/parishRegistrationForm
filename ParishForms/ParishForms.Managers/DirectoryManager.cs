using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Managers
{
    public class DirectoryManager : IDirectoryManager
    {
        #region Constructor and Private Members
        private readonly IDirectoryEngine _directoryEngine;

        public DirectoryManager(IDirectoryEngine directoryEngine)
        {
            _directoryEngine = directoryEngine
                ?? throw new ArgumentNullException(nameof(directoryEngine));
        }
        #endregion

        #region Contract Impl
        public async Task<IEnumerable<StateDto>> GetStateList()
        {
            return await _directoryEngine.GetStates();
        }

        public async Task<int> StoreSubmision(SubmisionDto submision)
        {
            return await _directoryEngine.StoreSubmision(submision);
        }
        #endregion
    }
}
