using System;
using System.Collections.Generic;
using System.IO;
using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using DataProvider.EntityFrameworkCore.Entities.Logging;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Logging;

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

        internal static LogHeaderEntity ToEntity(this ExceptionLogDto dto)
        {
            if (dto == null)
                return null;

            return new LogHeaderEntity
            {
                Level = (int) dto.Level,
                Timestamp = DateTimeOffset.UtcNow,
                Details = dto.Ex.ToEntities()
            };
        }

        internal static ICollection<LogDetailEntity> ToEntities(this Exception ex)
        {
            if(ex == null)
                return new List<LogDetailEntity>();

            var ents = new List<LogDetailEntity>
            {
                new LogDetailEntity
                {
                    EventType = (int) EventType.ExceptionMessage,
                    EventText = ex.Message
                },
                new LogDetailEntity
                {
                    EventType = (int) EventType.StackTrace,
                    EventText = ex.StackTrace
                }
            };

            if(ex.InnerException != null)
                ents.Add(new LogDetailEntity
                {
                    EventType = (int) EventType.InnerMessage,
                    EventText = ex.InnerException.Message
                });

            return ents;
        }

        internal static Type ToEntityType(this Type dtoType)
        {
            switch (dtoType.FullName)
            {
                case "ParishForms.Common.Models.CultureDto":
                    return typeof(CultureEntity);
                case "ParishForms.Common.Models.Directory.SubmisionDto":
                    return typeof(SubmisionEntitiy);
                case "ParishForms.Common.Models.Common.AddressDto":
                    return typeof(AddressEntity);
                case "ParishForms.Common.Models.Common.EmailDto":
                    return typeof(EmailAddressEntity);
                case "ParishForms.Common.Models.Common.PhoneDto":
                    return typeof(PhoneEntity);
                case "ParishForms.Common.Models.Common.StateDto":
                    return typeof(StateEntity);
                default:
                    return typeof(object);
            }
        }
    }
}
