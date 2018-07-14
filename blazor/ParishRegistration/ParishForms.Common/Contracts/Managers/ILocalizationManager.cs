using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Contracts.Managers
{
    public interface ILocalizationManager
    {
        Task<IEnumerable<StateDto>> GetStates();

        Task PreLoadCache();

        Task<IEnumerable<TranslationDto>> GetTranslations(string culture);

        Task<IEnumerable<CultureDto>> GetCultureList();
    }
}
