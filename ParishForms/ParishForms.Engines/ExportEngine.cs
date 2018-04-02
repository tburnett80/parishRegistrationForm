using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Engines
{
    public sealed class ExportEngine : IExportEngine
    {
        #region Constructor and Private members
        private readonly IExportAccessor _exportAccessor;

        public ExportEngine(IExportAccessor exportAccessor)
        {
            _exportAccessor = exportAccessor
                ?? throw new ArgumentNullException(nameof(exportAccessor));
        }
        #endregion

        #region Export Impl
        public async Task<ExportRequestDto> QueueRequest(int userId, string email)
        {
            var req = CreateNewExportRequest(userId, email);
            return await _exportAccessor.QueueRequest(req);
        }

        public async Task<ExportResultDto> CheckStatus(Guid requestId)
        {
            var queue = (await _exportAccessor.GetOpenItems()).ToList();

            var req = queue.FirstOrDefault(itm => itm.RequestId == requestId);
            if(req == null)
                return new ExportResultDto
                {
                    IsSuccessResult = false,
                    Message = "Could not find request matching this id.",
                    Request = new ExportRequestDto
                    {
                        RequestId = requestId,
                        ExportType = ExportRequestType.Unspecified,
                        Status = ExportStatus.NotFound
                    }
                };

            var position = queue.FindIndex(itm => itm.RequestId == requestId);
            return new ExportResultDto
            {
                IsSuccessResult = true,
                Message = $"There are {queue.Count} exports queued. '{requestId}' is #{position}, ({queue.Count - position} ahead of this one)",
                Request = req
            };
        }
        #endregion

        #region Private methods
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
