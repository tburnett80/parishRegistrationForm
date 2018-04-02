using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface IExportAccessor
    {
        Task<ExportRequestDto> QueueRequest(ExportRequestDto request);

        Task<ExportRequestDto> GetRequestByGuid(Guid requestId);

        Task<IEnumerable<ExportRequestDto>> GetOpenItems();

        Task<ExportRequestDto> GetNextOpenItem();

        Task<int> UpdateItem(ExportRequestDto dto);
    }
}
