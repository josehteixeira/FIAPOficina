using FIAPOficina.Application.Authentication.Commands.Login;
using FIAPOficina.Application.Users.Services;
using Microsoft.Extensions.Configuration;

namespace FIAPOficina.Application.Authentication.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly LoginCommandHandler _loginCommand;

        public AuthenticationService(IConfiguration configuration, IUsersService usersService)
        {
            _loginCommand = new LoginCommandHandler(configuration, usersService);
        }

        public async Task<string?> AuthenticateUser(string username, string password)
        {
            return await _loginCommand.Handle(new(username, password));
        }
    }
}