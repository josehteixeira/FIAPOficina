using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Users.Commands.AuthenticateUser;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Commands.DeleteUser;
using FIAPOficina.Application.Users.Commands.GetAllUsers;
using FIAPOficina.Application.Users.Commands.GetSingleUser;
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
        private readonly GetSingleUserCommandHandler _querySingleHandler;
        private readonly GetAllUsersCommandHandler _queryAllHandler;
        private readonly ValidateUserPasswordCommandHandler _validateUserPasswordHandler;

        public UsersService(IUserRepository repository, IPasswordHasherService passwordHasher)
        {
            _createHandler = new CreateUserCommandHandler(repository, passwordHasher);
            _updateHandler = new UpdateUserCommandHandler(repository);
            _deleteHandler = new DeleteUserCommandHandler(repository);
            _querySingleHandler = new GetSingleUserCommandHandler(repository);
            _queryAllHandler = new GetAllUsersCommandHandler(repository);
            _validateUserPasswordHandler = new ValidateUserPasswordCommandHandler(repository, passwordHasher);
        }

        public async Task<User> AddAsync(CreateUserCommand command)
        {
            return await _createHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<User> UpdateAsync(UpdateUserCommand command)
        {
            return await _updateHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task DeleteAsync(DeleteUserCommand command)
        {
            await _deleteHandler.Handle(command).ConfigureAwait(false);
        }

        public async Task<User?> GetSingleAsync(GetSingleUserCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public User[] GetAll(GetAllUsersCommand command)
        {
            return _queryAllHandler.Handle(command);
        }

        public async Task<bool> ValidateUserPassword(ValidateUserPasswordCommand command)
        {
            return await _validateUserPasswordHandler.Handle(command);
        }
    }
}