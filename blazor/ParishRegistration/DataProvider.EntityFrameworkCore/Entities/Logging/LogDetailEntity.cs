
namespace DataProvider.EntityFrameworkCore.Entities.Logging
{
    public class LogDetailEntity
    {
        public int Id { get; set; }

        public int HeaderId { get; set; }

        public int EventType { get; set; }

        public string EventText { get; set; }

        public virtual LogHeaderEntity Header { get; set; }
    }
}
