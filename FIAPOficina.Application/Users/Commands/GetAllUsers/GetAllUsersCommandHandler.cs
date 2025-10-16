using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Users.Commands.GetAllUsers
{
    public class GetAllUsersCommandHandler
    {
        private readonly IUserRepository _repository;

        public GetAllUsersCommandHandler(IUserRepository repository)
        {
            _repository = repository;
        }

        public User[] Handle(GetAllUsersCommand command)
        {
            return _repository.GetAll();
        }
    }
}