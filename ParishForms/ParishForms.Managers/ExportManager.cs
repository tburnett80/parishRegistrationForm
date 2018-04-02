using System;
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

        public async Task<ExportResultDto> ExportDirectoryResults()
        {
            try
            {
                return new ExportResultDto
                {
                    IsSuccessResult = true,
                    Request = await _directoryExportEngine.QueueRequest(0, string.Empty)
                };
            }
            catch (Exception)
            {
                //TODO: Log errors
                return new ExportResultDto
                {
                    IsSuccessResult = false
                };
            }
        }

        public Task<ExportResultDto> CheckStatus(Guid requestId)
        {
            return null;
        }
    }
}
