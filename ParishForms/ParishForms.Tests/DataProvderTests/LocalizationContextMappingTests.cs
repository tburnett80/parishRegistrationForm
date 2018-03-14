using System.Collections.Generic;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using DataProvider.EntityFrameworkCore.Entities.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParishForms.Tests.DataProvderTests
{
    [TestClass]
    public class LocalizationContextMappingTests
    {
        /// <summary>
        /// This test validates the context factory creates a sqlite database
        /// inserts test data, and we can retrieve the test data.
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task TestLocalizationMapping1()
        {
            //Arrange
            var contextFactory = new SqliteInMemoryContextFactory<LocalizationContext>();

            //Act
            using (var ctx = contextFactory.ConstructContext())
            {
                var recs = await ctx.Translations.ToListAsync();

                //Assert
                Assert.IsNotNull(recs, "should be 5 records");
                Assert.IsInstanceOfType(recs, typeof(List<LocalizationValueEntity>), "Should be true");
                Assert.AreEqual(5, recs.Count, "Should be 5 records");
            }
        }

        /// <summary>
        /// This test validates the context factory creates a sqlite database
        /// inserts test data, and we can retrieve the test data. 
        /// Should persist data after connection closes / disposed
        /// </summary>
        /// <returns></returns>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task TestLocalizationMapping2()
        {
            //Arrange
            using (var contextFactory = new SharedSqliteInMemoryContextFactory<LocalizationContext>())
            {
                //Act
                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs = await ctx.Translations.ToListAsync();

                    //Assert
                    Assert.IsNotNull(recs, "should be 5 records");
                    Assert.IsInstanceOfType(recs, typeof(List<LocalizationValueEntity>), "Should be true");
                    Assert.AreEqual(5, recs.Count, "Should be 5 records");
                }

                using (var ctx = contextFactory.ConstructContext())
                {
                    ctx.Translations.Add(new LocalizationValueEntity
                    {
                        KeyText = "Sample",
                        KeyCultureId = 1,
                        TranslationCultureId = 2,
                        TranslationText = "Muestra"
                    });

                    var ct = await ctx.SaveChangesAsync(true);

                    //Assert
                    Assert.AreEqual(1, ct, "Should have saved 1 record.");
                }

                using (var ctx = contextFactory.ConstructContext())
                {
                    var recs2 = await ctx.Translations.ToListAsync();

                    //Assert
                    Assert.IsNotNull(recs2, "should be 6 records");
                    Assert.IsInstanceOfType(recs2, typeof(List<LocalizationValueEntity>), "Should be true");
                    Assert.AreEqual(6, recs2.Count, "Should be 6 records now");
                }
            }
        }
    }
}
