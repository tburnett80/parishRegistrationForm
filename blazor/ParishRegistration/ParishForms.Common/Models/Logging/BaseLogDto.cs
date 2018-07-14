
namespace ParishForms.Common.Models.Logging
{
    public enum LogLevel
    {
        Info = 1,
        Warning,
        Verbose,
        Error,
    }

    public enum EventType
    {
        Message = 0,
        ExceptionMessage,
        InnerMessage,
        StackTrace
    }

    public abstract class BaseLogDto
    {
        public LogLevel Level { get; set; }
    }
}
