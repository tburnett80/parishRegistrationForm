
namespace DataProvider.EntityFrameworkCore.Entities.Common
{
    public class AddressEntity
    {
        public int Id { get; set; }

        public int AddressType { get; set; }

        public int StateId { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public virtual StateEntity State { get; set; }
    }
}
