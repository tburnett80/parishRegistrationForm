using ParishForms.Common.Models.Common;

namespace ParishForms.Common.Models.Directory
{
    public sealed class SubmisionDto
    {
        public bool PublishPhone { get; set; }

        public bool PublishAddress { get; set; }

        public string FamilyName { get; set; }

        public string AdultOneFirstName { get; set; }

        public string AdultTwoFirstName { get; set; }

        public string OtherFamily { get; set; }

        public PhoneDto HomePhone { get; set; }

        public AddressDto HomeAddress { get; set; }

        public PhoneDto AdultOneMobilePhone { get; set; }

        public PhoneDto AdultTwoMobilePhone { get; set; }

        public EmailDto AdultOneEmailAddress { get; set; }

        public EmailDto AdultTwoEmailAddress { get; set; }
    }
}
