using FIAPOficina.Application.Common.Mail;
using MimeKit;

namespace FIAPOficina.Infrastructure.Mail
{
    public class MailService : IMailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        private readonly bool _smtpSsl;
        private string _sourceMail;
        private string _subject;

        public MailService()
        {
            _smtpServer = Environment.GetEnvironmentVariable("SMTPSERVER") ?? throw new ArgumentNullException("Invalid SMTP Server");
            Int32.TryParse(Environment.GetEnvironmentVariable("SMTPPORT"), out _smtpPort);
            _smtpUser = Environment.GetEnvironmentVariable("SMTPUSER") ?? throw new ArgumentNullException("Invalid SMTP User");
            _smtpPassword = Environment.GetEnvironmentVariable("SMTPPASSWORD") ?? throw new ArgumentNullException("Invalid SMTP Password");
            _smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTPSSL") ?? throw new ArgumentNullException("Invalid SMTP Password"));
            
        }

        private MimeMessage CreateMailMessage(string destinationMail, string body)
        {

            var mailMessage = new MimeMessage();
            mailMessage.From.Add(new MailboxAddress(_sourceMail, _sourceMail));
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

        public bool SendMail(string sourceMail, string subject, string destinationMail, string body)
        {
            _sourceMail = sourceMail;
            _subject = subject;
           var mail = CreateMailMessage(destinationMail, body);
            return SendMail(mail);
        }
    }
}
