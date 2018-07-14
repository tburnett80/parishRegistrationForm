using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Tests.IntegrationTests
{
    [TestClass]
    public class DirectoryManagerTests : IntegrationTestBase
    {
        #region With Database
        /// <summary>
        /// This test scenario tests the everything works path.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Integration Test")]
        [TestCategory("With Database")]
        public async Task SubmitFormTest1()
        {
            //Arrange
            using (var dirCtxFactory = new SharedSqliteInMemoryContextFactory<DirectoryContext>())
            using (var logCtxFactory = new SharedSqliteInMemoryContextFactory<LogContext>())
            using (var logCtx = logCtxFactory.ConstructContext())
            {
                await logCtx.Database.ExecuteSqlCommandAsync("delete from detail");
                await logCtx.Database.ExecuteSqlCommandAsync("delete from header");

                var manager = FactoryManager(null, logCtxFactory, dirCtxFactory);

                var request = new SubmisionDto
                {
                    FamilyName = "testerson",
                    AdultOneFirstName = "test",
                    HomePhone = new PhoneDto
                    {
                        PhoneType = PhoneType.Home,
                        Number = "6365555555"
                    },
                    HomeAddress = new AddressDto
                    {
                        Street = "123 Main",
                        City = "Hometown",
                        Zip = "90210",
                        AddressType = AddressType.Home,
                        State = new StateDto
                        {
                            Abbreviation = "MO"
                        }
                    },
                };

                //Act
                var result = await manager.StoreSubmision(request);

                //Assert
                Assert.IsNotNull(result, "should always respond.");
                Assert.AreEqual(ResultType.Success, result.Type, "Should succeed");
                Assert.IsFalse(logCtx.LogHeaders.Any(), "Should be no log entries.");
            }
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        [TestCategory("With Database")]
        public async Task SubmitFormTest2()
        {
            //Arrange
            using (var dirCtxFactory = new SharedSqliteInMemoryContextFactory<DirectoryContext>())
            using (var logCtxFactory = new SharedSqliteInMemoryContextFactory<LogContext>())
            using (var logCtx = logCtxFactory.ConstructContext())
            {
                await logCtx.Database.ExecuteSqlCommandAsync("delete from detail");
                await logCtx.Database.ExecuteSqlCommandAsync("delete from header");

                var manager = FactoryManager(null, logCtxFactory, dirCtxFactory);

                var request = new SubmisionDto
                {
                    FamilyName = "testerson",
                    AdultOneFirstName = "test",
                    HomePhone = new PhoneDto
                    {
                        PhoneType = PhoneType.Home,
                        Number = "6365555555"
                    },
                    HomeAddress = new AddressDto
                    {
                        Street = "123 Main",
                        City = "Hometown",
                        Zip = "90210",
                        AddressType = AddressType.Home,
                        State = new StateDto
                        {
                            Abbreviation = "GH"
                        }
                    },
                };

                //Act
                var result = await manager.StoreSubmision(request);

                //Assert
                Assert.IsNotNull(result, "should always respond.");
                Assert.AreEqual(ResultType.Exception, result.Type, "Should succeed");
                Assert.AreEqual("Could not match state: GH", result.Message, "Should be equal");
                Assert.AreEqual(1, logCtx.LogHeaders.Count(), "Should be a msg logged after this");
                Assert.AreEqual(2, logCtx.LogDetails.Count(), "Should be two details logged after this");
            }
        }

        #endregion
    }
}
