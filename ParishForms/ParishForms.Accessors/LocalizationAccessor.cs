﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataProvider.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.DataProviders;
using ParishForms.Common.Models.Common;

namespace ParishForms.Accessors
{
    public sealed class LocalizationAccessor : ILocalizationAccessor
    {
        #region Constructor and Private Members
        private readonly IDbContextFactory<LocalizationContext> _contextFactory;

        public LocalizationAccessor(IDbContextFactory<LocalizationContext> contextFactory)
        {
            _contextFactory = contextFactory
                ?? throw new ArgumentNullException(nameof(contextFactory));
        }
        #endregion

        public async Task<IEnumerable<StateDto>> GetStates()
        {
            using (var ctx = _contextFactory.ConstructContext())
            {
                var ents = await ctx.States.ToListAsync();
                return ents.Select(e => e.ToDto());
            }
        }
    }
}
