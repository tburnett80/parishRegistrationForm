
using System.Threading.Tasks;
using ParishForms.Common.Models.Logging;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface ILogAccessor
    {
        Task LogException(ExceptionLogDto dto);
    }
}
