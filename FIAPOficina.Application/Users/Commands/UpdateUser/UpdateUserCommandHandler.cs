using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.UpdateUser
{
    internal class UpdateUserCommandHandler
    {
        private readonly IUserRepository _repository;

        public UpdateUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User> Handle(UpdateUserCommand command)
        {
            var user = new User(command.Name, command.UserName, command.Id);

            await _repository.UpdateAsync(user);

            return user;
        }
    }
}