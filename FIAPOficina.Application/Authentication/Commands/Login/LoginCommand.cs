namespace FIAPOficina.Application.Authentication.Commands.Login
{
    public class LoginCommand
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public LoginCommand(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }
}