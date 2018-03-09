
namespace DataProvider.EntityFrameworkCore.Entities.Logging
{
    public sealed class LogDetailEntity
    {
        public int Id { get; set; }

        public int HeaderId { get; set; }

        public int EventType { get; set; }

        public string EventText { get; set; }
    }
}
