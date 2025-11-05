namespace FIAPOficina.Application.Common.Mail
{
    public interface IMailService
    {
        bool SendMail(string subject, string destinationMail, string body);
    }
}