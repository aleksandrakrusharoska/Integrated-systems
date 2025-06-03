using LibraryDomain.Email;
using LibraryService.Interfaces;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using MimeKit.Text;

namespace LibraryService.Implementations
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;

        public EmailService(IOptions<EmailSettings> emailSettings)
        {
            this._emailSettings = emailSettings.Value;
        }

        public async Task SendEmail(EmailMessage message)
        {
            var email = new MimeMessage
            {
                Subject = message.Subject,
                Body = new TextPart(TextFormat.Plain)
                {
                    Text = message.Body
                },
                To = { new MailboxAddress(message.To, message.To) }
            };

            try
            {
                using var smtp = new SmtpClient();

                await smtp.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.SmtpPort);

                await smtp.AuthenticateAsync(_emailSettings.SmtpUsername, _emailSettings.SmtpPassword);

                await smtp.SendAsync(email);

                await smtp.DisconnectAsync(true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}