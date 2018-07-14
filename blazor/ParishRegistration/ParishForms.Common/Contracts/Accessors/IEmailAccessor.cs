using System.Threading.Tasks;
using ParishForms.Common.Models;

namespace ParishForms.Common.Contracts.Accessors
{
    public interface IEmailAccessor
    {
        Task SendEmail(EmailMessageDto message);
    }
}
