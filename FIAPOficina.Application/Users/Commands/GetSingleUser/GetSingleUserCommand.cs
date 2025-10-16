namespace FIAPOficina.Application.Users.Commands.GetSingleUser
{
    public class GetSingleUserCommand
    {
        public Guid Id { get; private set; }
        public string? Username { get; private set; }

        public GetSingleUserCommand(Guid id) { Id = id; }

        public GetSingleUserCommand(string username) { Username = username; }
    }
}