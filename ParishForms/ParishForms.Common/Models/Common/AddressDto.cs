
namespace ParishForms.Common.Models.Common
{
    public enum AddressType
    {
        Unspecified = 0,
        Home,
        Work
    }

    public sealed class AddressDto
    {
        public AddressType AddressType { get; set; }

        public string Street { get; set; }

        public string City { get; set; }

        public string Zip { get; set; }

        public StateDto State { get; set; }
    }
}
