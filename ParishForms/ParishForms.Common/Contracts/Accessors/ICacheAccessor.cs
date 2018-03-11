
using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface ICacheAccessor
    {
        IEnumerable<StateDto> GetStates();

        Task CacheStates(IEnumerable<StateDto> states);
    }
}
