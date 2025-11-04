using FIAPOficina.Application.Users.Commands.AuthenticateUser;
using FIAPOficina.Application.Users.Commands.CreateUser;
using FIAPOficina.Application.Users.Commands.DeleteUser;
using FIAPOficina.Application.Users.Commands.GetAllUsers;
using FIAPOficina.Application.Users.Commands.GetSingleUser;
using FIAPOficina.Application.Users.Commands.UpdateUser;
using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Application.Users.Services
{
    public interface IUsersService
    {
        Task<User> AddAsync(CreateUserCommand command);
        Task<User> UpdateAsync(UpdateUserCommand command);
        Task DeleteAsync(DeleteUserCommand command);
        Task<User?> GetSingleAsync(GetSingleUserCommand command);
        User[] GetAll(GetAllUsersCommand command);
        Task<bool> ValidateUserPassword(ValidateUserPasswordCommand command);
    }
}