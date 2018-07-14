using System;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Contracts.Managers;

namespace ParishForms.Managers
{
    public sealed class ExportProcessingManager : IExportProcessingManager
    {
        #region Constructor and Private members
        private readonly IExportProcessingEngine _engine;

        public ExportProcessingManager(IExportProcessingEngine engine)
        {
            _engine = engine
                ?? throw new ArgumentNullException(nameof(engine));
        }
        #endregion

        public async Task StartProcessing()
        {
            while (true)
            {
                await _engine.ProcessNext();
                var tsk = Task.Delay(30000);
                tsk.Wait();
            }
        }
    }
}
