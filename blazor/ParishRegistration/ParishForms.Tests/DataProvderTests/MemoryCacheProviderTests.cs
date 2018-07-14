using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataProvider.Cache;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ParishForms.Tests.DataProvderTests
{
    [TestClass]
    public class MemoryCacheProviderTests
    {
        #region DataCached Check Tests
        /// <summary>
        /// This test verifies the check for data existing functions correctly
        /// Nothing in cache so it should return false for a check
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheExistsTest1()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.DataCached("test1");

            //Assert
            Assert.IsFalse(result, "Should not be anything cached under that key");
        }

        /// <summary>
        /// This validates when data is chaced, a check for data should return true.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheExistsTest2()
        {
            //Arrange
            var cache = new MemoryCache();
            await cache.CacheObject("test2", new List<string> { "sample" });

            //Act
            var result = cache.DataCached("test2");

            //Assert
            Assert.IsTrue(result, "Should be an object cached under this key");
        }

        /// <summary>
        /// This scenario demonstrates invalidation of keys works
        /// and invalid items do not show as available.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheExistsTest3()
        {
            //Arrange
            var cache = new MemoryCache();
            await cache.CacheObject("test3", new List<string> { "timed out" }, 5);

            //Act
            Task.Delay(5500).Wait(); //Give item chance to expire
            var result = cache.DataCached("test3");

            //Assert
            Assert.IsFalse(result, "Should not be anything cached under that key, because its expired.");
        }

        /// <summary>
        /// This test validates a null or empty key always returns false
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheExistsTest4()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.DataCached(" ");

            //Assert
            Assert.IsFalse(result, "Empty key should always return false");
        }
        #endregion

        #region Invalidation Tests
        /// <summary>
        /// Scenario validates nothing should be removed from cache 
        /// when no data exists for a key
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheInvalidationTest1()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.InvalidateKey("test1");

            //Assert
            Assert.IsFalse(result, "no key so nothing to invalidate");
        }

        /// <summary>
        /// Scenario validates empty strings always return false
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheInvalidationTest2()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.InvalidateKey(" ");

            //Assert
            Assert.IsFalse(result, "null / empty strings should always return false");
        }

        /// <summary>
        /// Validates when a key matches it removes the entry
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheInvalidationTest3()
        {
            //Arrange
            var cache = new MemoryCache();
            await cache.CacheObject("test3", new List<string> { "sample" });

            //Act
            var result = cache.InvalidateKey("test3");

            //Assert
            Assert.IsTrue(result, "key matched, removed item.");
        }
        #endregion

        #region Cache object Tests
        /// <summary>
        /// Validates null / empty key results in nothing being cached
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheStoreObjectTest1()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = await cache.CacheObject(" ", new List<string> { "Sample" });

            //Assert
            Assert.IsFalse(result, "no key so nothing is cached");
        }

        /// <summary>
        /// null objects are not cached, this test validates that.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheStoreObjectTest2()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = await cache.CacheObject("test2", (object) null);

            //Assert
            Assert.IsFalse(result, "object was null, so nothing was cached");
        }

        /// <summary>
        /// This test validates the minimum cache length of 5 seconds is met
        /// nothing will be cached for less than 5 seconds
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheStoreObjectTest3()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = await cache.CacheObject("test3", new List<string> { "Sample" }, 4);

            //Assert
            Assert.IsFalse(result, "cache ttl below minimum threshold so nothing cached.");
        }

        /// <summary>
        /// Item should be cached and retrivable by its key.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheStoreObjectTest4()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = await cache.CacheObject("test4", new List<string> { "Sample" });

            //Assert
            Assert.IsTrue(result, "Item should be in the cache now.");
        }
        #endregion

        #region Get objects from cache Tests
        /// <summary>
        /// Null / empty keys should always return null.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheGetObjectTest0()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.GetObjectFromCache<List<string>>(" ");

            //Assert
            Assert.IsNull(result, "null or empty key should always return null.");
        }

        /// <summary>
        /// Nothing is cached for this key so we should get null
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public void MemoryCacheGetObjectTest1()
        {
            //Arrange
            var cache = new MemoryCache();

            //Act
            var result = cache.GetObjectFromCache<List<string>>("test1");

            //Assert
            Assert.IsNull(result, "nothing cached so we should get null");
        }

        /// <summary>
        /// data is cached, but the wrong cast is used so we get null
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheGetObjectTest2()
        {
            //Arrange
            var cache = new MemoryCache();
            var cached = await cache.CacheObject("test2", new List<string> { "sample" });

            //Act
            var result = cache.GetObjectFromCache<Dictionary<string, string>>("test2");

            //Assert
            Assert.IsTrue(cached, "Should have cached our string collection");
            Assert.IsNull(result, "Invalid Cast results in null");
        }

        /// <summary>
        /// This scenario validates the expiration of cached data functions as expected
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheGetObjectTest3()
        {
            //Arrange
            var cache = new MemoryCache();
            var cached = await cache.CacheObject("test3", new List<string> { "sample" }, 5);

            //Act
            Task.Delay(5200).Wait(); //Let cache expire
            var result = cache.GetObjectFromCache<List<string>>("test3");

            //Assert
            Assert.IsTrue(cached, "Should have cached our string collection");
            Assert.IsNull(result, "Data cache ttl expired, should return null.");
        }

        /// <summary>
        /// This test should validate typical expected retrival.
        /// Key matches, type matches, data has not expired so should retrieve.
        /// </summary>
        [TestMethod]
        [TestCategory("Unit Test")]
        public async Task MemoryCacheGetObjectTest4()
        {
            //Arrange
            var cache = new MemoryCache();
            var cached = await cache.CacheObject("test4", new List<string> { "sample success" });

            //Act
            var result = cache.GetObjectFromCache<List<string>>("test4");

            //Assert
            Assert.IsTrue(cached, "Should have cached our string collection");
            Assert.IsNotNull(result, "Should successfully retrieve data from cache");
            Assert.IsInstanceOfType(result, typeof(List<string>), "Should cast to propper type");
            Assert.AreEqual("sample success", result.FirstOrDefault(), "Should be equal");
        }
        #endregion
    }
}