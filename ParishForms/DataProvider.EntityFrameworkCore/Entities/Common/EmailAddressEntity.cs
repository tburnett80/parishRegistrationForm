
namespace DataProvider.EntityFrameworkCore.Entities.Common
{
    public sealed class EmailAddressEntity
    {
        public int Id { get; set; }

        public int EmailType { get; set; }

        public string Email { get; set; }
    }
}
