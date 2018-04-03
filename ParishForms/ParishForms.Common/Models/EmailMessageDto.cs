
namespace ParishForms.Common.Models
{
    public sealed class EmailMessageDto
    {
        public EmailMessageDto()
        {
            File = new byte[0];
        }

        public string To { get; set; }

        public string From { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public string AttatchmentMime { get; set; }

        public string FileName { get; set; }

        public byte[] File { get; set; }
    }
}
