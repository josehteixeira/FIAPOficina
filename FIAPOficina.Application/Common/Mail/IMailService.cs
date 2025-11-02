using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Common.Mail
{
    public interface IMailService
    {
        bool SendMail(string sourceMail, string subject, string destinationMail, string body);
    }
}
