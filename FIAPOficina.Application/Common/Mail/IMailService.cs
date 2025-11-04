namespace FIAPOficina.Application.Common.Mail
{
    public interface IMailService
    {
        bool SendMail(string sourceMail, string subject, string destinationMail, string body);
    }
}