
namespace ParishForms.Common.Models.Common
{
    public enum EmailType
    {
        Unspecified = 0,
        Personal,
        Work
    }

    public sealed class EmailDto
    {
        public EmailType EmailType { get; set; }

        public string Address { get; set; }
    }
}
