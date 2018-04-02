
using System.Threading.Tasks;

namespace ParishForms.Common.Contracts.Engines
{
    public interface IExportProcessingEngine
    {
        Task ProcessNext();
    }
}
