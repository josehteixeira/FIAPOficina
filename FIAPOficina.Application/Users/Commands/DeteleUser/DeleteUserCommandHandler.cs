using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.DeteleUser
{
    internal class DeleteUserCommandHandler
    {
        private readonly IUserRepository _repository;

        public DeleteUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task Handle(DeleteUserCommand command)
        {
            await _repository.DeleteAsync(command.Id);
        }
    }
}