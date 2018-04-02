using System.Threading.Tasks;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Managers
{
    public interface IDirectoryExportManager
    {
        Task<ExportResultDto> ExportDirectoryResults();
    }
}
