using System;
using System.Threading.Tasks;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface IExportAccessor
    {
        Task<ExportRequestDto> QueueRequest(ExportRequestDto request);

        Task<ExportRequestDto> GetRequestByGuid(Guid requestId);
    }
}
