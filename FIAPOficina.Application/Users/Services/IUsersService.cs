using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Application.Users.Services
{
    public interface IUsersService
    {
        public Task<User> AddAsync(CreateUserCommand command);
    }
}