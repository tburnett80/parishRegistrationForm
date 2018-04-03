using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.IO.Compression;
using System.Threading.Tasks;
using OfficeOpenXml;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Contracts.Engines;
using ParishForms.Common.Models;
using ParishForms.Common.Models.Directory;
using ParishForms.Common.Models.Exports;
using CompressionLevel = System.IO.Compression.CompressionLevel;

namespace ParishForms.Engines
{
    public sealed class ExportProcessingEngine : IExportProcessingEngine
    {
        #region Constructor and Private members
        private readonly IExportAccessor _exportAccessor;
        private readonly IDirectoryAccessor _directoryAccessor;
        private readonly IEmailAccessor _emailAccessor;
        private readonly ICacheAccessor _cache;

        public ExportProcessingEngine(IExportAccessor exportAccessor, IDirectoryAccessor directoryAccessor, IEmailAccessor emailAccessor, ICacheAccessor cache)
        {
            _exportAccessor = exportAccessor
                ?? throw new ArgumentNullException(nameof(exportAccessor));

            _directoryAccessor = directoryAccessor
                ?? throw new ArgumentNullException(nameof(directoryAccessor));

            _emailAccessor = emailAccessor
                ?? throw new ArgumentNullException(nameof(emailAccessor));

            _cache = cache
                ?? throw new ArgumentNullException(nameof(cache));
        }
        #endregion

        public async Task ProcessNext()
        {
            var nextItm = await _exportAccessor.GetNextOpenItem();
            var lastId = await _directoryAccessor.GetLastId();

            if (nextItm == null)
                return;

            var cached = _cache.GetCachedExport(nextItm.ExportType);
            if (cached != null && cached.End >= lastId)
                await SendResultEmail(nextItm, cached.Data);
            
            //TODO: if we have most of the data cached already, shouldnt we just
            //TODO: append whats new to this rather than creating it all again?

            nextItm.Status = ExportStatus.Started;
            await _exportAccessor.UpdateItem(nextItm);

            using(var ms = new MemoryStream())
            using (var pck = new ExcelPackage(ms))
            using(var wb = pck.Workbook.Worksheets.Add(nextItm.ExportType.ToString()))
            {
                var table = CreateDataTable();
                foreach (var chunk in ChunkRanges(nextItm.StartRange, lastId))
                {
                    foreach (var row in await _directoryAccessor.GetSubmisionsInRange(chunk.start, chunk.end))
                        AddRow(table, row);
                }

                wb.Cells["A1"].LoadFromDataTable(table, true);
                var zip = await CompressExcelFile(ms);

                nextItm.Status = ExportStatus.ExcelBuilt;
                await _exportAccessor.UpdateItem(nextItm);

                await _cache.CacheExport(nextItm.ExportType, new CompressedResult
                {
                    Start = nextItm.StartRange,
                    End = lastId,
                    Data = zip
                });

                await SendResultEmail(nextItm, zip);

                nextItm.Status = ExportStatus.Finished;
                await _exportAccessor.UpdateItem(nextItm);
            }
        }

        #region Private Methods
        /// <summary>
        /// this method builds a collection of id chunks to use as query ranges.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="stop"></param>
        /// <param name="chunkSize"></param>
        /// <returns></returns>
        private IEnumerable<dynamic> ChunkRanges(int start, int stop, int chunkSize = 100)
        {
            //determin the total number of items to chunk
            var total = stop - start;

            //get the number of full chunk sizes
            var iterations = total / chunkSize;
            if (total % chunkSize != 0) //check for a final partial chunk
                iterations++;

            //create the preset start and end number of each chunk
            var chunks = new List<dynamic>();
            for (var ndx = 1; ndx <= iterations; ndx++)
            {
                chunks.Add(new
                {
                    start = start + ((ndx - 1) * chunkSize),
                    end = ndx == iterations
                        ? stop
                        : start + (ndx * chunkSize) - 1
                });
            }

            return chunks;
        }

        private DataTable CreateDataTable()
        {
            var dt = new DataTable();
           
            dt.Columns.Add("Household Name", typeof(string));
            dt.Columns.Add("Adult 1", typeof(string));
            dt.Columns.Add("Adult 2", typeof(string));
            dt.Columns.Add("Children and Others", typeof(string));
            dt.Columns.Add("Publish Address", typeof(string));
            dt.Columns.Add("Publish Phone", typeof(string));
            dt.Columns.Add("Home Phone", typeof(string));
            dt.Columns.Add("Street Address", typeof(string));
            dt.Columns.Add("City", typeof(string));
            dt.Columns.Add("State", typeof(string));
            dt.Columns.Add("Zip", typeof(string));
            dt.Columns.Add("Adult 1 Mobile", typeof(string));
            dt.Columns.Add("Adult 1 Email", typeof(string));
            dt.Columns.Add("Adult 2 Mobile", typeof(string));
            dt.Columns.Add("Adult 2 Email", typeof(string));

            return dt;
        }

        private void AddRow(DataTable table, SubmisionDto dto)
        {
            var row = table.NewRow();

            row[0] = dto.FamilyName;
            row[1] = dto.AdultOneFirstName;
            row[2] = dto.AdultTwoFirstName;
            row[3] = dto.OtherFamily;
            row[4] = dto.PublishAddress.ToString();
            row[5] = dto.PublishPhone.ToString();
            row[6] = dto.HomePhone.Number; //TODO: format like nnn-nnn-nnnn
            row[7] = dto.HomeAddress.Street;
            row[8] = dto.HomeAddress.City;
            row[9] = dto.HomeAddress.State.Name;
            row[10] = dto.HomeAddress.Zip;
            row[11] = dto.AdultOneMobilePhone.Number; //TODO: format like nnn-nnn-nnnn
            row[12] = dto.AdultOneEmailAddress.Address;
            row[13] = dto.AdultTwoMobilePhone.Number; //TODO: format like nnn-nnn-nnnn
            row[14] = dto.AdultTwoEmailAddress.Address;

            table.Rows.Add(row);
        }

        private async Task<byte[]> CompressExcelFile(MemoryStream ms)
        {
            return await Task.Factory.StartNew(() =>
            {
                using (ms)
                using (var cms = new MemoryStream())
                using (var zip = new ZipArchive(cms, ZipArchiveMode.Create, false))
                {
                    var entry = zip.CreateEntry($"export-{DateTime.Now:yyyy-MM-dd}.xlsx", CompressionLevel.Optimal);
                    using (var entStr = entry.Open())
                        ms.CopyTo(entStr);

                    return cms.ToArray();
                }
            });
        }

        private async Task SendResultEmail(ExportRequestDto dto, byte[] zip)
        {
            await _emailAccessor.SendEmail(new EmailMessageDto
            {
                To = dto.Email,
                From = "no-reply@borromeoparish.directory", //TODO pull from settings
                Subject = $"Export Result {dto.RequestId}",
                Body = $"Request Id: '{dto.RequestId}' is complete! <br />Check this message for an attatchment.",
                FileName = $"Export-{DateTime.Now:yyyy-MM-dd}.zip",
                AttatchmentMime = "application/zip",
                File = zip
            });
        }
        #endregion
    }
}
