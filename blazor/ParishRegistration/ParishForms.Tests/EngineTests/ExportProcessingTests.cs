using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Exports;
using ParishForms.Engines;

namespace ParishForms.Tests.EngineTests
{
    [TestClass]
    public class ExportProcessingTests
    {
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task DatafoundToExport()
        {
            //Arrange
            var exportAccessor = new Mock<IExportAccessor>();

            exportAccessor.Setup(m => m.GetNextOpenItem())
                .ReturnsAsync(new ExportRequestDto
                {
                    Id = 1,
                    RequestId = Guid.NewGuid(),
                    ExportType = ExportRequestType.Directory,
                    Status = ExportStatus.InQueue,
                    Email = "test@test.com",
                    StartRange = 0
                });

            var directoryAccessor = new Mock<IDirectoryAccessor>();

            directoryAccessor.Setup(m => m.GetLastId())
                .ReturnsAsync(3);

            directoryAccessor.Setup(m => m.GetSubmisionsInRange(It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync((int start, int end) => GenerateSubmisions(end));

            var resultCollection = new List<EmailMessageDto>();
            var emailAccessor = new Mock<IEmailAccessor>();
            emailAccessor.Setup(m => m.SendEmail(It.IsAny<EmailMessageDto>()))
                .Returns((EmailMessageDto dto) =>
                {
                    resultCollection.Add(dto);
                    return Task.FromResult(0);
                });

            var cacheAccessor = new Mock<ICacheAccessor>();
            
            var engine = new ExportProcessingEngine(exportAccessor.Object, directoryAccessor.Object, emailAccessor.Object, cacheAccessor.Object);

            //Act
            await engine.ProcessNext();

            //Assert
            Assert.AreEqual(1, resultCollection.Count, "Should contain a single email.");

            //Validate the email is expected, contains the same file.
            var email = resultCollection.FirstOrDefault();
            emailAccessor.Verify(m => m.SendEmail(It.Is<EmailMessageDto>(e => 
                e.FileName.Equals(email.FileName)
                && e.File.Length == email.File.Length)), 
                Times.Once, "Should send a single email");
        }

        #region Private helper methods
        private IEnumerable<SubmisionDto> GenerateSubmisions(int count)
        {
            var subs = new List<SubmisionDto>();

            for(var ndx = 0; ndx < count; ndx++)
                subs.Add(new SubmisionDto
                {
                    PublishPhone = true,
                    PublishAddress = true,
                    FamilyName = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 8),
                    AdultOneFirstName = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12),
                    HomeAddress = new AddressDto
                    {
                        Street = "123 Main",
                        AddressType = AddressType.Home,
                        City = "home town",
                        Zip = "12345",
                        State = new StateDto
                        {
                            Name   = "Missouri",
                            Abbreviation = "MO"
                        }
                    },
                    HomePhone = new PhoneDto
                    {
                        Number = "555-556-5556"
                    },
                    AdultOneMobilePhone = new PhoneDto
                    {
                        Number = "555-555-5555"
                    },
                    AdultOneEmailAddress = new EmailDto
                    {
                        Address = $"test{ndx}@test.com"
                    }
                });

            return subs;
        }
        #endregion
    }
}
