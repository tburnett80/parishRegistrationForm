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
        NotFound = 0,
        InQueue,
        Started,
        Finished,
        Errored,
        Canceled
    }

    public sealed class ExportRequestDto
    {
        public ExportRequestDto()
        {
            TimeStamp = DateTimeOffset.MinValue;
        }

        public int Id { get; set; }

        public Guid RequestId { get; set; }

        public ExportRequestType ExportType { get; set; }

        public ExportStatus Status { get; set; }

        public DateTimeOffset TimeStamp { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public int StartRange { get; set; }
    }
}
