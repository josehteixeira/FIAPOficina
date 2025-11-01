namespace FIAPOficina.Application.Users.Commands.AuthenticateUser
{
    public class ValidateUserPasswordCommand
    {
        public string Username { get; private set; }
        public string Password { get; private set; }

        public ValidateUserPasswordCommand(string userName, string password)
        {
            Username = userName;
            Password = password;
        }
    }
}