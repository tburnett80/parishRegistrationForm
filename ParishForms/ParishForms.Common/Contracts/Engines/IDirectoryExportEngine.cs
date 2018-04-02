using System.Threading.Tasks;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Engines
{
    public interface IDirectoryExportEngine
    {
        Task<ExportRequestDto> QueueRequest(int userId, string email);
    }
}
