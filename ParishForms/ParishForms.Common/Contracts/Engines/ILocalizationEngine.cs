using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Contracts.Engines
{
    public interface ILocalizationEngine
    {
        Task<IEnumerable<StateDto>> GetStates();
    }
}
