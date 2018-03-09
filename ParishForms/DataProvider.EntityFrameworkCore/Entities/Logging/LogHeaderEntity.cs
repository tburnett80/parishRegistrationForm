using System;

namespace DataProvider.EntityFrameworkCore.Entities.Logging
{
    public sealed class LogHeaderEntity
    {
        public int Id { get; set; }

        public int Level { get; set; }

        public DateTimeOffset Timestamp { get; set; }
    }
}
