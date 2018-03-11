using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface ILocalizationAccessor
    {
        Task<IEnumerable<StateDto>> GetStates();

        Task<IEnumerable<string>> GetListOfCultures();

        Task<IEnumerable<TranslationDto>> GetTranslations(string culture);
    }
}
