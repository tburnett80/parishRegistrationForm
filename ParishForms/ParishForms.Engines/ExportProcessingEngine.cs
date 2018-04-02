using System;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Models.Exports;

namespace ParishForms.Engines
{
    public sealed class ExportProcessingEngine : IExportProcessingEngine
    {
        #region Constructor and Private members
        private readonly IExportAccessor _exportAccessor;

        public ExportProcessingEngine(IExportAccessor exportAccessor)
        {
            _exportAccessor = exportAccessor
                ?? throw new ArgumentNullException(nameof(exportAccessor));
        }
        #endregion

        public async Task ProcessNext()
        {
            var nextItm = await _exportAccessor.GetNextOpenItem();

            if (nextItm == null)
                return;

            nextItm.Status = ExportStatus.Started;
            await _exportAccessor.UpdateItem(nextItm);

            //TODO: get list of Ids
            //TODO: break up into chunks of 100
            //TODO: send to excel stream
            //TODO: update request
            //TODO: cache result
            //TODO: send result via email
            
        }
    }
}
