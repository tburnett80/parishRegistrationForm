using System.Collections.Generic;
using System.Threading.Tasks;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface ICacheAccessor
    {
        IEnumerable<StateDto> GetStates();

        Task CacheStates(IEnumerable<StateDto> states);

        IEnumerable<TranslationDto> GetTranslations(string culture);

        Task CacheTranslations(IEnumerable<TranslationDto> trans);

        IEnumerable<CultureDto> GetCultures();

        Task CacheCultures(IEnumerable<CultureDto> cultures);

        IDictionary<string, int> GetDirectoryFormLimits();

        Task CacheDirectoryFormLimits(IDictionary<string, int> limits);

        CompressedResult GetCachedExport(ExportRequestType type);

        Task CacheExport(ExportRequestType type, CompressedResult result);
    }
}
