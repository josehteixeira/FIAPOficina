using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User?> FirstOrDefaultAsync(Guid id);
        Task<User?> FirstOrDefaultAsync(string username);
        User[] GetAll();
    }
}