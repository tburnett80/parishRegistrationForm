using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using DataProvider.EntityFrameworkCore.Entities.Common;
using DataProvider.EntityFrameworkCore.Entities.Directory;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParishForms.Tests.DataProvderTests
{
    [TestClass]
    public class DirectoryContextMappingTests
    {
        /// <summary>
        /// This test validates the context factory creates a sqlite database
        /// inserts test data, and we can retrieve the test data.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task TestDirectoryMapping1()
        {
            //Arrange
            var contextFactory = new SqliteInMemoryContextFactory<DirectoryContext>();

            //Act
            using (var ctx = contextFactory.ConstructContext())
            {
                var rec = await ctx.Submisions
                    .Include(e => e.HomeAddress)
                    .ThenInclude(add => add.State)
                    .Include(e => e.HomePhone)
                    .Include(e => e.AdultTwoMobilePhone)
                    .Include(e => e.AdultTwoMobilePhone)
                    .Include(e => e.AdultOneEmail)
                    .Include(e => e.AdultTwoEmail)
                    .FirstOrDefaultAsync(s => s.Id == 1);

                //Assert
                Assert.IsNotNull(rec, "should be 1 record");
                Assert.IsInstanceOfType(rec, typeof(SubmisionEntitiy), "Should be true");
                
                Assert.IsNotNull(rec.HomeAddress, "Should load this child table");
                Assert.IsNotNull(rec.HomeAddress.State, "Should load this child table");
                Assert.IsNotNull(rec.HomePhone, "Should load this child table");
                Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                Assert.IsNotNull(rec.AdultOneEmail, "Should load this child table");
                Assert.IsNotNull(rec.AdultTwoEmail, "Should load this child table");
            }
        }

        /// <summary>
        /// This test validates the context factory creates a sqlite database
        /// inserts test data, and we can retrieve the test data.
        /// Data should persiste beyond the context being disposed
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task TestDirectoryMapping2()
        {
            //Arrange
            using (var contextFactory = new SharedSqliteInMemoryContextFactory<DirectoryContext>())
            {
                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs = await ctx.Submisions
                        .Include(e => e.HomeAddress)
                        .ThenInclude(add => add.State)
                        .Include(e => e.HomePhone)
                        .Include(e => e.AdultTwoMobilePhone)
                        .Include(e => e.AdultTwoMobilePhone)
                        .Include(e => e.AdultOneEmail)
                        .Include(e => e.AdultTwoEmail)
                        .ToListAsync();

                    var rec = recs.FirstOrDefault();

                    //Assert
                    Assert.IsNotNull(recs, "should be 1 record");
                    Assert.IsInstanceOfType(recs, typeof(List<SubmisionEntitiy>), "Should be true");
                    Assert.AreEqual(1, recs.Count, "should be 1 record");

                    Assert.IsNotNull(rec, "should be 1 record");
                    Assert.IsInstanceOfType(rec, typeof(SubmisionEntitiy), "Should be true");
                    Assert.AreEqual(1, rec.Id, "Should be record #1");

                    Assert.IsNotNull(rec.HomeAddress, "Should load this child table");
                    Assert.IsNotNull(rec.HomeAddress.State, "Should load this child table");
                    Assert.IsNotNull(rec.HomePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultOneEmail, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoEmail, "Should load this child table");
                }

                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    await ctx.Submisions.AddAsync(new SubmisionEntitiy
                    {
                        AdultOneFirstName = "MoarTest",
                        AdultTwoFirstName = "YesTest",
                        FamilyName = "Testington",
                        OtherFamily = "Test",
                        PublishAddress = true,
                        PublishPhone = false,
                        AdultOneMobilePhone = new PhoneEntity
                        {
                            TypeId = 2,
                            Number = "405-555-4321"
                        },
                        AdultTwoMobilePhone = new PhoneEntity
                        {
                            TypeId = 2,
                            Number = "405-555-5678"
                        },
                        HomePhone = new PhoneEntity
                        {
                            TypeId = 1,
                            Number = "405-555-1234"
                        },
                        AdultTwoEmail = new EmailAddressEntity
                        {
                            EmailType = 1,
                            Email = "test.2@test.com"
                        },
                        AdultOneEmail = new EmailAddressEntity
                        {
                            EmailType = 1,
                            Email = "test.3@test.com"
                        },
                        HomeAddress = new AddressEntity
                        {
                            StateId = 1,
                            City = "Oklahoma City",
                            Street = "3233 Sw 94th St.",
                            Zip = "73159"
                        }
                    });

                    var ct = await ctx.SaveChangesAsync(true);

                    //Assert
                    Assert.AreEqual(7, ct, "Seven classes means 7 records. Would be 8 if we created a new state as well.");
                }

                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs = await ctx.Submisions
                        .Include(e => e.HomeAddress)
                        .ThenInclude(add => add.State)
                        .Include(e => e.HomePhone)
                        .Include(e => e.AdultTwoMobilePhone)
                        .Include(e => e.AdultTwoMobilePhone)
                        .Include(e => e.AdultOneEmail)
                        .Include(e => e.AdultTwoEmail)
                        .OrderBy(e => e.Id)
                        .ToListAsync();

                    var rec = recs.LastOrDefault();

                    //Assert
                    Assert.IsNotNull(recs, "should be 2 records");
                    Assert.IsInstanceOfType(recs, typeof(List<SubmisionEntitiy>), "Should be true");
                    Assert.AreEqual(2, recs.Count, "should be 2 records");

                    Assert.IsNotNull(rec, "should be 1 record");
                    Assert.IsInstanceOfType(rec, typeof(SubmisionEntitiy), "Should be true");
                    Assert.AreEqual(2, rec.Id, "Should be record #2");

                    Assert.IsNotNull(rec.HomeAddress, "Should load this child table");
                    Assert.IsNotNull(rec.HomeAddress.State, "Should load this child table");
                    Assert.IsNotNull(rec.HomePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoMobilePhone, "Should load this child table");
                    Assert.IsNotNull(rec.AdultOneEmail, "Should load this child table");
                    Assert.IsNotNull(rec.AdultTwoEmail, "Should load this child table");
                }
            }
        }
    }
}
