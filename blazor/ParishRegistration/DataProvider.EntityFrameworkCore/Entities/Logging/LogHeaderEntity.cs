using System;
using System.Collections.Generic;

namespace DataProvider.EntityFrameworkCore.Entities.Logging
{
    public class LogHeaderEntity
    {
        public LogHeaderEntity()
        {
            Details = new List<LogDetailEntity>();
        }

        public int Id { get; set; }

        public int Level { get; set; }

        public DateTimeOffset? Timestamp { get; set; }

        public virtual ICollection<LogDetailEntity> Details { get; set; }
    }
}
