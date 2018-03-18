using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Cache;
using DataProvider.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParishForms.Accessors;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Engines;
using ParishForms.Managers;

namespace ParishForms.Tests.IntegrationTests
{
    [TestClass]
    public class DirectoryManagerTests : IntegrationTestBase
    {
        #region With Database

        [TestMethod]
        [TestCategory("Integration Test")]
        [TestCategory("With Database")]
        public async Task SubmitFormTest1()
        {
            //Arrange
            var dirCtxFactory = new SharedSqliteInMemoryContextFactory<DirectoryContext>();
            var logCtxFactory = new SharedSqliteInMemoryContextFactory<LogContext>();

            var manager = FactoryManager(null, logCtxFactory, dirCtxFactory);
            
            var request = new SubmisionDto
            {
                FamilyName = "testerson",
                AdultOneFirstName = "test",
                HomePhone = new PhoneDto
                {
                    PhoneType = PhoneType.Home,
                    Number = "636-555-5555"
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
        }

        #endregion
    }
}
