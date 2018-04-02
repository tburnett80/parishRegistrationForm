using System;

namespace ParishForms.Common.Models.Exports
{
    public enum ExportRequestType
    {
        Unspecified = 0,
        Directory
    }

    public enum ExportStatus
    {
        InQueue = 0,
        Started,
        Finished,
        Errored,
        Canceled
    }

    public sealed class ExportRequestDto
    {
        public ExportRequestDto()
        {
            TimeStamp = DateTimeOffset.UtcNow;
        }

        public Guid RequestId { get; set; }

        public ExportRequestType ExportType { get; set; }

        public ExportStatus Status { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public int StartRange { get; set; }
    }
}
