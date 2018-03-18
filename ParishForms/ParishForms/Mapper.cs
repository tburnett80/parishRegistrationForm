using ParishForms.Common.Extensions;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.ViewModels;

namespace ParishForms
{
    public static class Mapper
    {
        internal static SubmisionDto ToDto(this DirectoryFormViewModel model)
        {
            if (model == null)
                return null;

            return new SubmisionDto
            {
                AdultOneFirstName = model.Adult1FName.TryTrim(),
                AdultTwoFirstName = model.Adult2FName.TryTrim(),
                FamilyName = model.FamilyName.TryTrim(),
                OtherFamily = model.OtherNames.TryTrim(),
                PublishAddress = model.PublisAddress,
                PublishPhone = model.PublishPhone,
                HomeAddress = new AddressDto
                {
                    AddressType = AddressType.Home,
                    City = model.City.TryTrim(),
                    Street = model.Address.TryTrim(),
                    Zip = model.Zip.TryTrim(),
                    State = new StateDto { Abbreviation = model.State.TryToTrimedUpper() }
                },
                HomePhone = model.HomePhone.HasValue()
                    ? new PhoneDto { PhoneType = PhoneType.Home, Number = model.HomePhone.TryTrimRemove("-") }
                    : null,
                AdultOneMobilePhone = model.Adult1Cell.HasValue() 
                    ? new PhoneDto { PhoneType = PhoneType.Mobile, Number = model.Adult1Cell.TryTrimRemove("-") } 
                    : null,
                AdultTwoMobilePhone = model.Adult2Cell.HasValue()
                    ? new PhoneDto { PhoneType = PhoneType.Mobile, Number = model.Adult2Cell.TryTrimRemove("-") }
                    : null,
                AdultOneEmailAddress = model.Adult1Email.HasValue()
                    ? new EmailDto { EmailType = EmailType.Personal, Address = model.Adult1Email.TryTrim() }
                    : null,
                AdultTwoEmailAddress = model.Adult2Email.HasValue()
                    ? new EmailDto { EmailType = EmailType.Personal, Address = model.Adult2Email.TryTrim() }
                    : null,
            };
        }
    }
}
