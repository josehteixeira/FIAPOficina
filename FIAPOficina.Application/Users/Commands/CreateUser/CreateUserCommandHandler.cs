using FIAPOficina.Application.Common.Security;
using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.CreateUser
{
    internal class CreateUserCommandHandler
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasherService _hasher;

        public CreateUserCommandHandler(IUserRepository repository, IPasswordHasherService hasherService)
        {
            _repository = repository;
            _hasher = hasherService;
        }

        public async Task<User> Handle(CreateUserCommand command)
        {
            string passwordHash = _hasher.Hash(command.Password);

            var user = await _repository.AddAsync(
                user: new User(command.Name, command.UserName),
                passwordHash: passwordHash
            );

            return user;
        }
    }
}