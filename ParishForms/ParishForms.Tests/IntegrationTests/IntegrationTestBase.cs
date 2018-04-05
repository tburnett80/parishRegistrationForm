using DataProvider.Cache;
using DataProvider.EntityFrameworkCore;
using ParishForms.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Models;
using ParishForms.Engines;
using ParishForms.Managers;

namespace ParishForms.Tests.IntegrationTests
{
    public abstract class IntegrationTestBase
    {
        protected IDirectoryManager FactoryManager(ConfigSettingsDto settings = null, IDbContextFactory<LogContext> logCtxFactory = null, IDbContextFactory<DirectoryContext> dirCtxFactory = null, IDbContextFactory<ExportContext> xprtCtxFactory = null)
        {
            if(settings == null)
                settings = new ConfigSettingsDto
                {
                    StateCacheTtlSeconds = 1200,
                    TranslationCacheTtlSeconds = 1200
                };

            if (logCtxFactory == null)
                logCtxFactory = new SqliteInMemoryContextFactory<LogContext>();

            if(dirCtxFactory == null)
                dirCtxFactory = new SqliteInMemoryContextFactory<DirectoryContext>();

            if(xprtCtxFactory == null)
                xprtCtxFactory = new SqliteInMemoryContextFactory<ExportContext>();

            var cacheAccessor = new CacheAccessor(new MemoryCache(), settings);
            var dirAccessor = new DirectoryAccessor(dirCtxFactory);
            var logAccessor = new LogAccessor(logCtxFactory);
            var exportAccessor = new ExportAccessor(xprtCtxFactory);

            var dirEngine = new DirectoryEngine(dirAccessor, cacheAccessor, exportAccessor);
            return new DirectoryManager(dirEngine, logAccessor);
        }
    }
}
