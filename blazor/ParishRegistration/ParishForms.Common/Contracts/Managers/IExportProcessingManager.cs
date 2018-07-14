using System.Threading.Tasks;

namespace ParishForms.Common.Contracts.Managers
{
    public interface IExportProcessingManager
    {
        Task StartProcessing();
    }
}
