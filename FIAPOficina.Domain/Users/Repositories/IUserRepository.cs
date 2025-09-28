using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user, string passwordHash);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
    }
}