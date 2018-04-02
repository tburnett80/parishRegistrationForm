using System;
using System.Collections.Generic;
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

        public async Task<IEnumerable<ExportRequestDto>> GetOpenItems()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var itms = await ctx.ExportQueue
                    .Where(e => e.Status == (int) ExportStatus.InQueue || e.Status == (int) ExportStatus.Started)
                    .OrderBy(e => e.Id)
                    .ToListAsync();

                return itms.Select(e => e.ToDto());
            }
        }

        public async Task<ExportRequestDto> GetStartedItem()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var itm = await ctx.ExportQueue
                    .Where(e => e.Status == (int)ExportStatus.Started)
                    .OrderBy(e => e.Id)
                    .FirstOrDefaultAsync();

                return itm.ToDto();
            }
        }

        public async Task<ExportRequestDto> GetNextOpenItem()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var next = await ctx.ExportQueue
                    .Where(e => e.Status == (int)ExportStatus.Started || e.Status == (int)ExportStatus.InQueue)
                    .OrderBy(e => e.Status)
                    .ThenBy(e => e.Id)
                    .FirstOrDefaultAsync();

                return next.ToDto();
            }
        }

        public async Task<int> UpdateItem(ExportRequestDto dto)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                ctx.Update(dto.ToEntity());
                return await ctx.SaveChangesAsync(true);
            }
        }
    }
}
