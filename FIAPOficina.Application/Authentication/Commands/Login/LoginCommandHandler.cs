using FIAPOficina.Application.Users.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace FIAPOficina.Application.Authentication.Commands.Login
{
    internal class LoginCommandHandler
    {
        private readonly IConfiguration _configuration;
        private readonly IUsersService _usersService;

        public LoginCommandHandler(IConfiguration configuration, IUsersService usersService)
        {
            _configuration = configuration;
            _usersService = usersService;
        }

        public async Task<string?> Handle(LoginCommand command)
        {
            if (string.IsNullOrEmpty(command.Username))
                throw new ArgumentNullException("Username can not be empty!");

            if (string.IsNullOrEmpty(command.Password))
                throw new ArgumentNullException("Password can not be empty!");

            bool validLogin = await _usersService
                                    .ValidateUserPassword(new(command.Username, command.Password))
                                    .ConfigureAwait(false);

            if (validLogin)
            {
                return GenerateToken(command.Username);
            }

            return null;
        }

        private string GenerateToken(string username)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}