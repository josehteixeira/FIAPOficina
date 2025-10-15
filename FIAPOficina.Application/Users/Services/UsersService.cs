using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Commands.DeteleUser;
using FIAPOficina.Application.Users.Commands.UpdateUser;
using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly CreateUserCommandHandler _createHandler;
        private readonly UpdateUserCommandHandler _updateHandler;
        private readonly DeleteUserCommandHandler _deleteHandler;

        public UsersService(IUserRepository repository, IPasswordHasherService passwordHasher)
        {
            _createHandler = new CreateUserCommandHandler(repository, passwordHasher);
            _updateHandler = new UpdateUserCommandHandler(repository);
            _deleteHandler = new DeleteUserCommandHandler(repository);
        }

        public async Task<User> AddAsync(CreateUserCommand command)
        {
            return await _createHandler.Handle(command);
        }

        public async Task<User> UpdateAsync(UpdateUserCommand command)
        {
            return await _updateHandler.Handle(command);
        }

        public async Task DeleteAsync(DeleteUserCommand command)
        {
            await _deleteHandler.Handle(command);
        }
    }
}