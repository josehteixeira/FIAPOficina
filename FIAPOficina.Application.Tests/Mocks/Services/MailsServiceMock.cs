using FIAPOficina.Application.Common.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
