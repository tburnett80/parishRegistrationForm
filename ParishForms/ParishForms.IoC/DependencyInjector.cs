﻿using DataProvider.Cache;
using DataProvider.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ParishForms.Accessors;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;
using ParishForms.Common.Extensions;
using ParishForms.Common.Models;
using ParishForms.Engines;
using ParishForms.Managers;


namespace ParishForms.IoC
{
    public static class DependencyInjector
    {
        public static void AddServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ConfigSettingsDto>(new ConfigSettingsDto
            {
                ConnectionString = configuration["CONNECTION_STRING"],
                StateCacheTtlSeconds = configuration["STATE_CACHE_TTL"].TryToInt(),
                TranslationCacheTtlSeconds = configuration["TRANSLATION_CACHE_TTL"].TryToInt()
            });

            services.AddSingleton<ICacheProvider, MemoryCache>();
            services.AddTransient<IDbContextFactory<CreationContext>, PostgresContextFactory<CreationContext>>();
            services.AddTransient<IDbContextFactory<LocalizationContext>, PostgresContextFactory<LocalizationContext>>();
            services.AddTransient<IDbContextFactory<LogContext>, PostgresContextFactory<LogContext>>();
            services.AddTransient<IDbContextFactory<DirectoryContext>, PostgresContextFactory<DirectoryContext>>();

            services.AddTransient<ICacheAccessor, CacheAccessor>();
            services.AddTransient<ILocalizationAccessor, LocalizationAccessor>();
            services.AddTransient<IDirectoryAccessor, DirectoryAccessor>();

            services.AddTransient<ILocalizationEngine, LocalizationEngine>();
            services.AddTransient<IDirectoryEngine, DirectoryEngine>();

            services.AddTransient<ILocalizationManager, LocalizationManager>();
            services.AddTransient<IDirectoryManager, DirectoryManager>();
        }
    }
}