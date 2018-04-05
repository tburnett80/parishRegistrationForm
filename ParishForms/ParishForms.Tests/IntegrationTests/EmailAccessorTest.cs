using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ParishForms.Accessors;
using ParishForms.Common.Models;

namespace ParishForms.Tests.IntegrationTests
{
    [TestClass]
    public class EmailAccessorTest : TestBaseClass
    {
        [TestMethod]
        [TestCategory("Integration Test")]
        [Ignore]
        public async Task SendTestMail1()
        {
            //Arrange
            var settings = new ConfigSettingsDto
            {
                RelayAddress = ""
            };

            var accessor = new EmailAccessor(settings);

            var msg = new EmailMessageDto
            {
                To = "",
                From = "no-repy@",
                Subject = "Test",
                Body = "This is a test..."
            };

            //Act
            await accessor.SendEmail(msg);

            //Assert
        }

        [TestMethod]
        [TestCategory("Integration Test")]
        [Ignore]
        public async Task SendTestMail2()
        {
            //Arrange
            var settings = new ConfigSettingsDto
            {
                RelayAddress = "10.200.7.116"
            };

            var accessor = new EmailAccessor(settings);

            var msg = new EmailMessageDto
            {
                To = "",
                From = "no-repy@",
                Subject = "Test2",
                Body = "This is a test with zipped excel.",
                FileName = "sample.zip",
                AttatchmentMime = "application/zip",
                File = GetEmbededFileByName("stuff.zip")
            };

            //Act
            await accessor.SendEmail(msg);

            //Assert
        }
    }
}
