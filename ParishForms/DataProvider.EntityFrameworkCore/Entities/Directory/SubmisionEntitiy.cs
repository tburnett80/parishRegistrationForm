using System;

namespace DataProvider.EntityFrameworkCore.Entities.Directory
{
    public sealed class SubmisionEntitiy
    {
        public int Id { get; set; }

        public DateTimeOffset Timestamp { get; set; }

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
    }
}
