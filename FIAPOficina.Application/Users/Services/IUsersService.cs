using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Commands.DeteleUser;
using FIAPOficina.Application.Users.Commands.UpdateUser;
using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Application.Users.Services
{
    public interface IUsersService
    {
        public Task<User> AddAsync(CreateUserCommand command);
        public Task<User> UpdateAsync(UpdateUserCommand command);
        public Task DeleteAsync(DeleteUserCommand command);
    }
}