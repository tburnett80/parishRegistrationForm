using System;
using System.Collections.Generic;
using DataProvider.EntityFrameworkCore;
using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using DataProvider.EntityFrameworkCore.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.DataProviders;

namespace ParishForms.Tests
{
    /// <summary>
    /// This class should be used for tests that can dispose of their database when the context is disposed.
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public class SqliteInMemoryContextFactory<TContext> : IDbContextFactory<TContext> where TContext : DbContext, new()
    {
        protected readonly DbContextOptions Options;

        public SqliteInMemoryContextFactory()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseSqlite("DataSource=:memory:"); 
            Options = builder.Options;
        }

        public SqliteInMemoryContextFactory(DbContextOptions opts)
        {
            Options = opts;
        }

        public virtual TContext ConstructContext()
        {
            return InitTestData((TContext) Activator.CreateInstance(typeof(TContext), Options));
        }

        public virtual void Dispose()
        {
        }

        private TContext InitTestData(TContext ctx)
        {
            ctx.Database.OpenConnection();
            ctx.Database.EnsureCreated();

            if (typeof(TContext) == typeof(LogContext))
                return InitLogs(ctx);

            if (typeof(TContext) == typeof(LocalizationContext))
                return InitTranslations(ctx);

            return typeof(TContext) == typeof(DirectoryContext) 
                ? InitDirectory(ctx) 
                : ctx;
        }

        private TContext InitDirectory(TContext ctx)
        {
            ctx.AddRange(new []
            {
                new StateEntity
                {
                    Abbreviation = "MO",
                    Name = "Missouri"
                },
                new StateEntity
                {
                    Abbreviation = "NV",
                    Name = "Nevada"
                },
                new StateEntity
                {
                    Abbreviation = "OH",
                    Name = "Ohio"
                },
                new StateEntity
                {
                    Abbreviation = "IL",
                    Name = "Illinois"
                }
            });
            ctx.SaveChanges(true);

            ctx.Add(new AddressEntity
            {
                AddressType = 1,
                City = "Saint Charles",
                StateId = 1,
                Street = "601 N 4th St",
                Zip = "63301"
            });

            ctx.AddRange(new []
            {
                new PhoneEntity
                {
                    TypeId = 1,
                    Number = "636-946-1893"
                },
                new PhoneEntity
                {
                    TypeId = 2,
                    Number = "636-555-1234"
                },
                new PhoneEntity
                {
                    TypeId = 2,
                    Number = "636-555-4321"
                },
            });

            ctx.AddRange(new []
            {
                new EmailAddressEntity
                {
                    EmailType = 1,
                    Email = "testy.testington@test.com"
                },
                new EmailAddressEntity
                {
                    EmailType = 1,
                    Email = "tester.testington@test.com"
                }
            });

            ctx.SaveChanges(true);

            ctx.Add(new SubmisionEntitiy
            {
                AddressId = 1,
                HomePhoneId = 1,
                AdultOnePhoneId = 2,
                AdultTwoPhoneId = 3,
                AdultOneEmailId = 1,
                AdultTwoEmailId = 2,
                PublishPhone = true,
                PublishAddress = true,
                FamilyName = "Testington",
                AdultOneFirstName = "Testy",
                AdultTwoFirstName = "Tester",
                OtherFamily = "Test,Testerton,Testing"
            });

            ctx.SaveChanges(true);
            return ctx;
        }

        private TContext InitTranslations(TContext ctx)
        {
            ctx.AddRange(new []
            {
                new LocalizationValueEntity
                {
                    KeyCulture = "en-us",
                    KeyText = "Household Name",
                    TranslationCulture = "es-mx",
                    TranslationText = "Familia"
                },
                new LocalizationValueEntity
                {
                    KeyCulture = "en-us",
                    KeyText = "Home Phone",
                    TranslationCulture = "es-mx",
                    TranslationText = "Teléfono de casa"
                },
                new LocalizationValueEntity
                {
                    KeyCulture = "en-us",
                    KeyText = "Street Address",
                    TranslationCulture = "es-mx",
                    TranslationText = "Dirección"
                },
                new LocalizationValueEntity
                {
                    KeyCulture = "en-us",
                    KeyText = "City",
                    TranslationCulture = "es-mx",
                    TranslationText = "Ciudad"
                },
                new LocalizationValueEntity
                {
                    KeyCulture = "en-us",
                    KeyText = "Email Address",
                    TranslationCulture = "es-mx",
                    TranslationText = "correo electronico"
                },
            });

            ctx.SaveChanges(true);
            return ctx;
        }

        private TContext InitLogs(TContext ctx)
        {
            ctx.Add(new LogHeaderEntity
            {
                Level = 1,
                Details = new List<LogDetailEntity>
                {
                    new LogDetailEntity
                    {
                        EventType = 1,
                        EventText = "Error message"
                    },
                    new LogDetailEntity
                    {
                        EventType = 3,
                        EventText = "Stack Trace"
                    },
                }
            });

            ctx.SaveChanges(true);
            return ctx;
        }
    }
}
