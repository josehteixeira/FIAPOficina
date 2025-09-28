using FIAPOficina.Application.Common.Security;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Services
{
    public class UsersService : IUsersService
    {
        private readonly CreateUserCommandHandler _createHandler;

        public UsersService(IUserRepository repository, IPasswordHasherService passwordHasher)
        {
            _createHandler = new CreateUserCommandHandler(repository, passwordHasher);
        }

        public async Task<User> AddAsync(CreateUserCommand command)
        {
            return await _createHandler.Handle(command);
        }
    }
}