using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Managers
{
    public sealed class ExportManager : IDirectoryExportManager
    {
        #region Constructor and Private members
        private readonly IDirectoryExportEngine _directoryExportEngine;

        public ExportManager(IDirectoryExportEngine directoryExportEngine)
        {
            _directoryExportEngine = directoryExportEngine
                ?? throw new ArgumentNullException(nameof(directoryExportEngine));
        }
        #endregion

        public Task<ExportResultDto> ExportDirectoryResults(bool onlyNew = false)
        {
            return null;
        }

        public Task<ExportResultDto> CheckStatus(Guid requestId)
        {
            return null;
        }
    }
}
