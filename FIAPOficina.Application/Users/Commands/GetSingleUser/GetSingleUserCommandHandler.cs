using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.GetSingleUser
{
    internal class GetSingleUserCommandHandler
    {
        private readonly IUserRepository _repository;

        public GetSingleUserCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> Handle(GetSingleUserCommand command)
        {
            if (string.IsNullOrEmpty(command.Username))
            {
                return await _repository.FirstOrDefaultAsync(command.Id).ConfigureAwait(false);
            }
            return await _repository.FirstOrDefaultAsync(command.Username).ConfigureAwait(false);
        }
    }
}