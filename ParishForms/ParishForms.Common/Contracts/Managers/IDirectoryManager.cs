using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Common.Contracts.Managers
{
    public interface IDirectoryManager
    {
        Task<IEnumerable<StateDto>> GetStateList();

        Task<SaveResult> StoreSubmision(SubmisionDto submision);

        Task<IDictionary<string, int>> GetFormLimits();
    }
}
