using System;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Managers
{
    public sealed class ExportManager : IExportManager
    {
        #region Constructor and Private members
        private readonly IExportEngine _exportEngine;
        private readonly IExportAccessor _accessor;

        public ExportManager(IExportEngine exportEngine, IExportAccessor accessor)
        {
            _exportEngine = exportEngine
                ?? throw new ArgumentNullException(nameof(exportEngine));

            _accessor = accessor 
                ?? throw new ArgumentNullException(nameof(accessor));
        }
        #endregion

        public async Task<ExportResultDto> ExportDirectoryResults()
        {
            try
            {
                var req = await _exportEngine.QueueRequest(0, string.Empty);
                return await _exportEngine.CheckStatus(req.RequestId);
            }
            catch (Exception ex)
            {
                //TODO: Log errors
                return new ExportResultDto
                {
                    IsSuccessResult = false,
                    Message = $"Error queueing request: {ex.Message}"
                };
            }
        }

        public async Task<ExportResultDto> CheckStatus(Guid requestId)
        {
            try
            {
                return new ExportResultDto
                {
                    IsSuccessResult = true,
                    Request = await _accessor.GetRequestByGuid(requestId)
                };
            }
            catch (Exception ex)
            {
                //TODO: Log errors
                return new ExportResultDto
                {
                    IsSuccessResult = false,
                    Message = $"Error checking status: {ex.Message}"
                };
            }
        }
    }
}
