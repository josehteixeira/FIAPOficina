using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

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
            Users createUser = new()
            {
                Id = user.Id == Guid.Empty ? Guid.NewGuid() : user.Id,
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
            var userToUpdate = _context.Users.FirstOrDefault(u => u.Id == user.Id);
            if (userToUpdate is not null)
            {
                userToUpdate.Name = user.Name;
                userToUpdate.UserName = user.UserName;

                _context.Users.Update(userToUpdate);

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var userToDelete = _context.Users.FirstOrDefault(c => c.Id == id);

            if (userToDelete is not null)
            {
                _context.Users.Remove(userToDelete);
            }

            await _context.SaveChangesAsync();
        }

        public async Task<User?> FirstOrDefaultAsync(Guid id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id).ConfigureAwait(false);

            if (user is not null)
            {
                return new User
                (
                    name: user.Name,
                    userName: user.UserName,
                    id: user.Id
                );
            }

            return null;
        }

        public async Task<User?> FirstOrDefaultAsync(string username)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == username).ConfigureAwait(false);

            if (user is not null)
            {
                return new User
                (
                    name: user.Name,
                    userName: user.UserName,
                    id: user.Id
                );
            }

            return null;
        }

        public User[] GetAll()
        {
            var users = _context.Users.ToArray();

            return users.Select(user =>
                new User
                (
                    user.Name,
                    user.UserName,
                    user.Id
                )).ToArray();
        }
    }
}