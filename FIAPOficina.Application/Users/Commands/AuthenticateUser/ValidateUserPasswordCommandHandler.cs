using FIAPOficina.Application.Common.Security;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.AuthenticateUser
{
    internal class ValidateUserPasswordCommandHandler
    {
        private readonly IUserRepository _repository;
        private readonly IPasswordHasherService _hasher;

        public ValidateUserPasswordCommandHandler(IUserRepository repository, IPasswordHasherService hasherService)
        {
            _repository = repository;
            _hasher = hasherService;
        }

        public async Task<bool> Handle(ValidateUserPasswordCommand command)
        {
            string passwordHash = _hasher.Hash(command.Password);

            var user = await _repository.FirstOrDefaultAsync(
                username: command.Username
            ).ConfigureAwait(false);

            return user is not null && _hasher.Verify(command.Password, user.PasswordHash ?? "");
        }
    }
}