using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models.Common;
using ParishForms.Common.Models.Directory;

namespace ParishForms.Accessors
{
    public sealed class DirectoryAccessor : IDirectoryAccessor
    {
        #region Constructor and private members
        private readonly IDbContextFactory<DirectoryContext> _contextFactory;

        public DirectoryAccessor(IDbContextFactory<DirectoryContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        public async Task<int> StoreSubmision(SubmisionDto submision)
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                await ctx.Submisions.AddAsync(submision.ToEntity());
                return await ctx.SaveChangesAsync(true);
            }
        }

        public async Task<IEnumerable<StateDto>> GetStates()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ents = await ctx.States.ToListAsync();
                return ents.Select(e => e.ToDto());
            }
        }

        public async Task<IDictionary<string, int>> GetFieldLengths<TEnt>() where TEnt : class
        {
            return await Task.Factory.StartNew(() =>
            {
                using (var ctx = _contextFactory.ConstructContext())
                {
                    var model = ctx.Model.GetEntityTypes()
                        .FirstOrDefault(t => t.Name.Equals(typeof(TEnt).ToEntityType().FullName));

                    return model?.GetProperties()
                               .Where(p => p.FindAnnotation("MaxLength") != null)
                               .ToDictionary(k => k.Name.ToLower(), v => (int)v.FindAnnotation("MaxLength").Value)
                           ?? new Dictionary<string, int>();
                }
            });
        }
    }
}
