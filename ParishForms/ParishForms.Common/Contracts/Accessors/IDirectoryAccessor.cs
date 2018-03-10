
using System.Threading.Tasks;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface IDirectoryAccessor
    {
        Task<int> StoreSubmision(SubmisionDto submision);
    }
}
