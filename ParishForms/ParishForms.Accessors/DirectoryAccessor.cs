using System;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
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

            }

            throw new NotImplementedException();
        }
    }
}
