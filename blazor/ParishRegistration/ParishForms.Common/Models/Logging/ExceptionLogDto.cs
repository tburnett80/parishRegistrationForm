using System;

namespace ParishForms.Common.Models.Logging
{
    public sealed class ExceptionLogDto : BaseLogDto
    {
        public ExceptionLogDto()
        {
            Level = LogLevel.Error;
        }

        public ExceptionLogDto(Exception ex)
        {
            Ex = ex;
            Level = LogLevel.Error;
        }

        public Exception Ex { get; set; }
    }
}
