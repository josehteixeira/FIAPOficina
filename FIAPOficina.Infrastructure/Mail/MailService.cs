using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

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


        private SmtpClient _smtpClient;
        public MailService(string sourceMail,string subject)
        {
            _smtpServer = Environment.GetEnvironmentVariable("SMTPSERVER") ?? throw new ArgumentNullException("Invalid SMTP Server");
            Int32.TryParse(Environment.GetEnvironmentVariable("SMTPPORT"), out _smtpPort);
            _smtpUser = Environment.GetEnvironmentVariable("SMTPUSER") ?? throw new ArgumentNullException("Invalid SMTP User");
            _smtpPassword = Environment.GetEnvironmentVariable("SMTPPASSWORD") ?? throw new ArgumentNullException("Invalid SMTP Password");
            _smtpSsl = bool.Parse(Environment.GetEnvironmentVariable("SMTPSSL") ?? throw new ArgumentNullException("Invalid SMTP Password"));
            _sourceMail = sourceMail;
            _subject = subject;
            Initialize();
        }
        private void Initialize()
        {
            _smtpClient = new SmtpClient(_smtpServer)
            {
                Port = _smtpPort,
                Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
                EnableSsl = _smtpSsl
            };
        }

        public MailMessage CreateMailMessage(string destinationMail,string body, bool isHTML)
        {
            var mailMessage = new MailMessage
            {
                From = new MailAddress(_sourceMail),
                Subject = _subject,
                Body = body,
                IsBodyHtml = true
            };
            mailMessage.To.Add(destinationMail);

            return mailMessage;
        }

        public bool SendMail(MailMessage message)
        {
            try
            {
                _smtpClient.Send(message);
                return true;
            }
            catch (SmtpException ex)
            {
                throw new Exception("Fail to send mail", ex);
            }
        }

    }
}
