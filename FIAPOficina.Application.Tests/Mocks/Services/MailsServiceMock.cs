using FIAPOficina.Application.Common.Mail;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class MailsServiceMock : IMailService
    {
        public bool SendMail(string sourceMail, string subject, string destinationMail, string body)
        {
            return true;
        }
    }
}
