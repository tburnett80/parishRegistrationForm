using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using DataProvider.EntityFrameworkCore.Entities.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParishForms.Tests.DataProvderTests
{
    [TestClass]
    public class LoggingContextMappingTests
    {
        /// <summary>
        /// This test validates the context factory creates a sqlite database
        /// inserts test data, and we can retrieve the test data.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task TestLoggingMapping1()
        {
            //Arrange
            var contextFactory = new SqliteInMemoryContextFactory<LogContext>();

            //Act
            using (var ctx = contextFactory.ConstructContext())
            {
                var recs = await ctx.LogHeaders
                    .Include(e => e.Details)
                    .ToListAsync();

                var rec = recs.FirstOrDefault();

                //Assert
                Assert.IsNotNull(recs, "should be 1 record");
                Assert.AreEqual(1, recs.Count, "should be 1 record");
                Assert.IsInstanceOfType(recs, typeof(List<LogHeaderEntity>), "Should be equal");

                Assert.IsNotNull(rec, "should be 1 record");
                Assert.IsInstanceOfType(rec, typeof(LogHeaderEntity), "Should be equal");
                Assert.IsNotNull(rec.Details, "should be 2 records");
                Assert.AreEqual(2, rec.Details.Count, "should be 2 records");
                Assert.IsInstanceOfType(rec.Details, typeof(ICollection<LogDetailEntity>), "Should be equal");
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
        public async Task TestLoggingMapping2()
        {
            //Arrange
            using (var contextFactory = new SharedSqliteInMemoryContextFactory<LogContext>())
            {
                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs = await ctx.LogHeaders
                        .Include(e => e.Details)
                        .ToListAsync();

                    var rec = recs.FirstOrDefault();

                    //Assert
                    Assert.IsNotNull(recs, "should be 1 record");
                    Assert.AreEqual(1, recs.Count, "should be 1 record");
                    Assert.IsInstanceOfType(recs, typeof(List<LogHeaderEntity>), "Should be equal");

                    Assert.IsNotNull(rec, "should be 1 record");
                    Assert.IsInstanceOfType(rec, typeof(LogHeaderEntity), "Should be equal");
                    Assert.AreEqual(1, rec.Id, "Should be record #1");
                    Assert.IsNotNull(rec.Details, "should be 2 records");
                    Assert.AreEqual(2, rec.Details.Count, "should be 2 records");
                    Assert.IsInstanceOfType(rec.Details, typeof(ICollection<LogDetailEntity>), "Should be equal");
                }

                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    await ctx.LogHeaders.AddAsync(new LogHeaderEntity
                    {
                        Level = 2, 
                        Details = new List<LogDetailEntity>
                        {
                            new LogDetailEntity
                            {
                                EventType = 4,
                                EventText = "Sample Info Msg"
                            }
                        }
                    });

                    var ct = await ctx.SaveChangesAsync(true);

                    //Assert
                    Assert.AreEqual(2, ct, "Should have added 2 records");
                }

                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs = await ctx.LogHeaders
                        .Include(e => e.Details)
                        .ToListAsync();

                    var rec = recs.LastOrDefault();

                    //Assert
                    Assert.IsNotNull(recs, "should be 2 records");
                    Assert.AreEqual(2, recs.Count, "should be 2 records");
                    Assert.IsInstanceOfType(recs, typeof(List<LogHeaderEntity>), "Should be equal");

                    Assert.IsNotNull(rec, "should be 1 record");
                    Assert.IsInstanceOfType(rec, typeof(LogHeaderEntity), "Should be equal");
                    Assert.AreEqual(2, rec.Id, "Should be record #2");
                    Assert.IsNotNull(rec.Details, "should be 1 record");
                    Assert.AreEqual(1, rec.Details.Count, "should be 1 record");
                    Assert.IsInstanceOfType(rec.Details, typeof(ICollection<LogDetailEntity>), "Should be equal");
                }
            }
        }
    }
}
