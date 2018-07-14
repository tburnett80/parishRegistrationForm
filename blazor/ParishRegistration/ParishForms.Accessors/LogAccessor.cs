using System;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models.Logging;

namespace ParishForms.Accessors
{
    public sealed class LogAccessor : ILogAccessor
    {
        #region Constructor and Private Members
        private readonly IDbContextFactory<LogContext> _contextFactory;

        public LogAccessor(IDbContextFactory<LogContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        public async Task LogException(ExceptionLogDto dto)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ent = dto.ToEntity();
                await ctx.AddAsync(ent);

                await ctx.SaveChangesAsync(true);
            }
        }
    }
}
