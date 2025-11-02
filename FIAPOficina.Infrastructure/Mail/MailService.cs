using MimeKit;

namespace FIAPOficina.Infrastructure.Mail
{
    public class MailService
    {
        private readonly string _smtpServer;
        private readonly int _smtpPort;
        private readonly string _smtpUser;
        private readonly string _smtpPassword;
        private readonly bool _smtpSsl;
        private readonly string _sourceMail;
        private readonly string _subject;

        public MailService(string sourceMail, string subject)
        {
            _smtpServer = Environment.GetEnvironmentVariable("SMTPSERVER") ?? throw new ArgumentNullException("Invalid SMTP Server");
            Int32.TryParse(Environment.GetEnvironmentVariable("SMTPPORT"), out _smtpPort);
            _smtpUser = Environment.GetEnvironmentVariable("SMTPUSER") ?? throw new ArgumentNullException("Invalid SMTP User");
            _smtpPassword = Environment.GetEnvironmentVariable("SMTPPASSWORD") ?? throw new ArgumentNullException("Invalid SMTP Password");
            _smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTPSSL") ?? throw new ArgumentNullException("Invalid SMTP Password"));
            _sourceMail = sourceMail;
            _subject = subject;
        }

        public MimeMessage CreateMailMessage(string destinationMail, string body)
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

        public bool SendMail(MimeMessage message)
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
    }
}
