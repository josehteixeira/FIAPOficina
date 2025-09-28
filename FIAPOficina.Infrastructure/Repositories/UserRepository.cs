using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddAsync(User user, string passwordHash)
        {
            // TODO: Create ID and password hash
            Users createUser = new()
            {
                Id = Guid.NewGuid(),
                Name = user.Name,
                UserName = user.UserName,
                PasswordHash = passwordHash,
            };

            _context.Users.Add(createUser);
            await _context.SaveChangesAsync();

            return new User(user, createUser.Id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(new()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(User user)
        {
            _context.Users.Remove(new()
            {
                Id = user.Id,
                Name = user.Name,
                UserName = user.UserName
            });

            await _context.SaveChangesAsync();
        }
    }
}