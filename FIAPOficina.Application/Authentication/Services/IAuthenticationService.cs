namespace FIAPOficina.Application.Authentication.Services
{
    public interface IAuthenticationService
    {
        Task<string?> AuthenticateUser(string username, string password);
    }
}