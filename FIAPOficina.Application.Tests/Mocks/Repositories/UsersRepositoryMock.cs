using FIAPOficina.Domain.Users.Entities;
using FIAPOficina.Domain.Users.Repositories;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class UsersRepositoryMock : IUserRepository
    {
        private List<User> _users = new List<User>();
        public async Task<User> AddAsync(User user)
        {
            _users.Add(user);
            return user;
        }

        public async Task DeleteAsync(Guid id)
        {
            var user = FirstOrDefaultAsync(id).GetAwaiter().GetResult();
            _users.Remove(user);
        }

        public async Task<User?> FirstOrDefaultAsync(Guid id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public async Task<User?> FirstOrDefaultAsync(string userName)
        {
            return _users.FirstOrDefault(u => u.UserName.Equals(userName));
        }

        public User[] GetAll()
        {
            return _users.ToArray();
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}
