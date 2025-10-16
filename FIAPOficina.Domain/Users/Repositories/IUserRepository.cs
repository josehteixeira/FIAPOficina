using FIAPOficina.Domain.Users.Entities;

namespace FIAPOficina.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user, string passwordHash);
        Task UpdateAsync(User user);
        Task DeleteAsync(Guid id);
        Task<User?> FirstOrDefaultAsync(Guid id);
        Task<User?> FirstOrDefaultAsync(string userName);
        User[] GetAll();
    }
}