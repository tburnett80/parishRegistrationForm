using System;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Accessors
{
    public sealed class ExportAccessor : IExportAccessor
    {
        #region Constructor and Private Members
        private readonly IDbContextFactory<ExportContext> _contextFactory;

        public ExportAccessor(IDbContextFactory<ExportContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        public async Task<ExportRequestDto> QueueRequest(ExportRequestDto request)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ent = await ctx.ExportQueue.AddAsync(request.ToEntity());
                await ctx.SaveChangesAsync(true);

                return ent.Entity.ToDto();
            }
        }

        public async Task<ExportRequestDto> GetRequestByGuid(Guid requestId)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ent = await ctx.ExportQueue
                    .Where(e => e.RequestId == requestId)
                    .FirstOrDefaultAsync();

                return ent.ToDto();
            }
        }
    }
}
