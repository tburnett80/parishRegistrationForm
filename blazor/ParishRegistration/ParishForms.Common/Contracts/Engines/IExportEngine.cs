using System;
using System.Threading.Tasks;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Engines
{
    public interface IExportEngine
    {
        Task<ExportRequestDto> QueueRequest(int userId, string email);

        Task<ExportResultDto> CheckStatus(Guid requestId);
    }
}
