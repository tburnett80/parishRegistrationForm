using System;

namespace DataProvider.EntityFrameworkCore.Entities.Exports
{
    public class ExportQueueEntity
    {
        public int Id { get; set; }

        public Guid RequestId { get; set; }

        public int ExportType { get; set; }

        public int Status { get; set; }

        public DateTimeOffset? TimeStamp { get; set; }

        public int UserId { get; set; }

        public string Email { get; set; }

        public int StartRange { get; set; }

        public DateTimeOffset? LastUpdated { get; set; }
    }
}
