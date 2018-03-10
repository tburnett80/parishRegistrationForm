
namespace ParishForms.Common.Models.Common
{
    public enum PhoneType
    {
        Unspecified = 0,
        Home,
        Mobile,
        Work,
        Fax
    }

    public sealed class PhoneDto
    {
        public PhoneType PhoneType { get; set; }

        public string Number { get; set; }
    }
}
