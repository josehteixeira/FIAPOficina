using FIAPOficina.Application.Common.Mail;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;

namespace FIAPOficina.Infrastructure.Mail
{
    public class MailService : IMailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        private string _subject;
        private readonly ILogger<MailService> _logger;

        public MailService(IConfiguration configuration, ILogger<MailService> logger)
        {
            _logger = logger;

            try
            {
                _smtpServer = configuration["SMTP:Server"] ?? throw new ArgumentNullException("Invalid SMTP Server");
                Int32.TryParse(configuration["SMTP:Port"] ?? throw new ArgumentNullException("Invalid SMTP Port"), out _smtpPort);
                _smtpUser = configuration["SMTP:User"] ?? throw new ArgumentNullException("Invalid SMTP User");
                _smtpPassword = configuration["SMTP:Password"] ?? throw new ArgumentNullException("Invalid SMTP Password");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to set SMTP settings, mail service will be unavailable!");
            }
        }

        private MimeMessage CreateMailMessage(string destinationMail, string body)
        {
            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_smtpUser, _smtpUser));
            mailMessage.To.Add(new MailboxAddress(destinationMail, destinationMail));
            mailMessage.Subject = _subject;

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.HtmlBody = body;
            mailMessage.Body = bodyBuilder.ToMessageBody();

            return mailMessage;
        }

        private bool SendMail(MimeMessage message)
        {
            try
            {

                using (var client = new MailKit.Net.Smtp.SmtpClient())
                {

                    client.Connect(_smtpServer, _smtpPort);

                    client.Authenticate(_smtpUser, _smtpPassword);

                    client.Send(message);
                    client.Disconnect(true);
                }
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Fail to send mail", ex);
            }
        }

        public bool SendMail(string subject, string destinationMail, string body)
        {
            _subject = subject;
            var mail = CreateMailMessage(destinationMail, body);
            return SendMail(mail);
        }
    }
}
