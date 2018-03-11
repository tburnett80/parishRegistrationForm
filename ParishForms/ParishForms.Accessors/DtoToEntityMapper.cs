using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Accessors
{
    internal static class DtoToEntityMapper
    {
        internal static SubmisionEntitiy ToEntity(this SubmisionDto dto)
        {
            if (dto == null)
                return null;

            return new SubmisionEntitiy
            {
                PublishAddress = dto.PublishAddress,
                PublishPhone = dto.PublishPhone,
                FamilyName = dto.FamilyName,
                AdultOneFirstName = dto.AdultOneFirstName,
                AdultTwoFirstName = dto.AdultTwoFirstName,
                OtherFamily = dto.OtherFamily,
                HomeAddress = dto.HomeAddress?.ToEntity(),
                HomePhone = dto.HomePhone?.ToEntity(),
                AdultOneMobilePhone = dto.AdultOneMobilePhone?.ToEntity(),
                AdultTwoMobilePhone = dto.AdultTwoMobilePhone?.ToEntity(),
                AdultOneEmail = dto.AdultOneEmailAddress?.ToEntity(),
                AdultTwoEmail = dto.AdultTwoEmailAddress?.ToEntity()
            };
        }

        internal static AddressEntity ToEntity(this AddressDto dto)
        {
            if (dto == null)
                return null;

            return new AddressEntity
            {
                AddressType = (int) dto.AddressType,
                Street = dto.Street,
                City = dto.City,
                Zip = dto.Zip,
                StateId = dto.State.Id
            };
        }

        internal static PhoneEntity ToEntity(this PhoneDto dto)
        {
            if (dto == null)
                return null;

            return new PhoneEntity
            {
                TypeId = (int) dto.PhoneType,
                Number = dto.Number
            };
        }

        internal static EmailAddressEntity ToEntity(this EmailDto dto)
        {
            if (dto == null)
                return null;

            return new EmailAddressEntity
            {
                EmailType = (int) dto.EmailType,
                Email = dto.Address
            };
        }
    }
}
