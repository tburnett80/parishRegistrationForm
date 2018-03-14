using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface IDirectoryAccessor
    {
        Task<IEnumerable<StateDto>> GetStates();

        Task<int> StoreSubmision(SubmisionDto submision);

        Task<IDictionary<string, int>> GetFieldLengths<TEnt>() where TEnt : class;
    }
}
