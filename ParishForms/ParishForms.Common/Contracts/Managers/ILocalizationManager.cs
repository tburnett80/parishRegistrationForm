using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Contracts.Managers
{
    public interface ILocalizationManager
    {
        Task<IEnumerable<StateDto>> GetStates();
    }
}
