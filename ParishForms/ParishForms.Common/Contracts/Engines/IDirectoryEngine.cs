using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Common.Contracts.Engines
{
    public interface IDirectoryEngine
    {
        Task<IEnumerable<StateDto>> GetStates();

        Task<int> StoreSubmision(SubmisionDto submision);

        Task<IDictionary<string, int>> GetFormLimits();

        bool ValidateSubmision(SubmisionDto submision);
    }
}
