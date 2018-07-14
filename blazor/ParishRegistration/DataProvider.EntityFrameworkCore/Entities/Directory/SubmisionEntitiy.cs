using System;
using DataProvider.EntityFrameworkCore.Entities.Common;

namespace DataProvider.EntityFrameworkCore.Entities.Directory
{
    public class SubmisionEntitiy
    {
        public int Id { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public int AddressId { get; set; }

        public int? HomePhoneId { get; set; }

        public int? AdultOnePhoneId { get; set; }

        public int? AdultOneEmailId { get; set; }

        public int? AdultTwoPhoneId { get; set; }

        public int? AdultTwoEmailId { get; set; }

        public bool PublishPhone { get; set; }

        public bool PublishAddress { get; set; }

        public string FamilyName { get; set; }

        public string AdultOneFirstName { get; set; }

        public string AdultTwoFirstName { get; set; }

        public string OtherFamily { get; set; }

        public virtual AddressEntity HomeAddress { get; set; }

        public virtual PhoneEntity HomePhone { get; set; }

        public virtual PhoneEntity AdultOneMobilePhone { get; set; }

        public virtual PhoneEntity AdultTwoMobilePhone { get; set; }

        public virtual EmailAddressEntity AdultOneEmail { get; set; }

        public virtual EmailAddressEntity AdultTwoEmail { get; set; }
    }
}
