using System;

namespace ParishForms.ViewModels
{
    public sealed class ExportQueueResultViewModel
    {
        public Guid ReqestId { get; set; }

        public string Status { get; set; }

        public string Message { get; set; }
    }
}
