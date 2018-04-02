using System;
using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using DataProvider.EntityFrameworkCore.Entities.Exports;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Accessors
{
    internal static class EntityToDtoMapper
    {
        internal static StateDto ToDto(this StateEntity ent)
        {
            if (ent == null)
                return null;

            return new StateDto
            {
                Id = ent.Id,
                Abbreviation = ent.Abbreviation,
                Name = ent.Name
            };
        }

        internal static TranslationDto ToDto(this LocalizationValueEntity ent)
        {
            if (ent == null)
                return null;

            return new TranslationDto
            {
                KeyCulture = ent.KeyCulture.CultureCode.ToLower(),
                LocalizedCulture = ent.TranslationCulture.CultureCode.ToLower(),
                KeyText = ent.KeyText,
                LocalizedText = ent.TranslationText
            };
        }

        internal static CultureDto ToDto(this CultureEntity ent)
        {
            if (ent == null)
                return null;

            return new CultureDto
            {
                Culture = ent.CultureCode,
                Name = ent.CultureName
            };
        }

        internal static ExportRequestDto ToDto(this ExportQueueEntity ent)
        {
            if (ent == null)
                return null;

            return new ExportRequestDto
            {
                RequestId = ent.RequestId,
                Email = ent.Email,
                UserId = ent.UserId,
                ExportType = (ExportRequestType) ent.ExportType,
                Status = (ExportStatus) ent.Status,
                StartRange = ent.StartRange,
                TimeStamp = ent.TimeStamp ?? DateTimeOffset.MinValue
            };
        }

        internal static PhoneDto ToDto(this PhoneEntity ent)
        {
            if (ent == null)
                return null;
            
            return new PhoneDto
            {
                Number = ent.Number,
                PhoneType = (PhoneType) ent.TypeId
            };
        }

        internal static EmailDto ToDto(this EmailAddressEntity ent)
        {
            if (ent == null)
                return null;
            
            return new EmailDto
            {
                EmailType = (EmailType) ent.EmailType,
                Address = ent.Email
            };
        }

        internal static AddressDto ToDto(this AddressEntity ent)
        {
            if (ent == null)
                return null;
            
            return new AddressDto
            {
                Street = ent.Street,
                AddressType = (AddressType) ent.AddressType,
                City = ent.City,
                Zip = ent.Zip,
                State = ent.State.ToDto()
            };
        }

        internal static SubmisionDto ToDto(this SubmisionEntitiy ent)
        {
            if (ent == null)
                return null;

            return new SubmisionDto
            {
                PublishPhone = ent.PublishPhone,
                PublishAddress = ent.PublishAddress,
                FamilyName = ent.FamilyName,
                OtherFamily = ent.OtherFamily,
                AdultOneFirstName = ent.AdultOneFirstName,
                AdultTwoFirstName = ent.AdultTwoFirstName,
                HomeAddress = ent.HomeAddress.ToDto(),
                HomePhone = ent.HomePhone.ToDto(),
                AdultOneMobilePhone = ent.AdultOneMobilePhone.ToDto(),
                AdultOneEmailAddress = ent.AdultOneEmail.ToDto(),
                AdultTwoMobilePhone = ent.AdultTwoMobilePhone.ToDto(),
                AdultTwoEmailAddress = ent.AdultTwoEmail.ToDto()
            };
        }
    }
}
