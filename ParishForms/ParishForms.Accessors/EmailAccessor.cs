using System;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using ParishForms.Common.Contracts.Accessors;
using ParishForms.Common.Models;

namespace ParishForms.Accessors
{
    public class EmailAccessor : IEmailAccessor
    {
        #region Constructor and private members
        private readonly string _relayAddress;

        public EmailAccessor(ConfigSettingsDto settings)
        {
            if(settings == null)
                throw new ArgumentNullException(nameof(settings));

            _relayAddress = settings.RelayAddress;
        }
        #endregion

        public async Task SendEmail(EmailMessageDto message)
        {
            await Task.Factory.StartNew(() =>
            {
                using (var client = new SmtpClient(_relayAddress))
                {
                    client.Port = 25;
                    client.UseDefaultCredentials = false;

                    var msg = new MailMessage(new MailAddress(message.From), new MailAddress(message.To))
                    {
                        Subject = message.Subject,
                        Body = message.Body
                    };

                    if (message.File.Any())
                        msg.Attachments.Add(new Attachment(new MemoryStream(message.File), message.FileName, message.AttatchmentMime));

                    client.Send(msg);
                }
            });
        }

        
    }
}
