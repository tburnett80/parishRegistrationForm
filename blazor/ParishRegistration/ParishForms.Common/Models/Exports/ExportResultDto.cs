
namespace ParishForms.Common.Models.Exports
{
    public sealed class ExportResultDto
    {
        public ExportResultDto()
        {
            Data = new byte[0];
        }

        public bool IsSuccessResult { get; set; }

        public ExportRequestDto Request { get; set; }

        public string Message { get; set; }

        public string FileName { get; set; }

        public byte[] Data { get; set; }
    }
}
