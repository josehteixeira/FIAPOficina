using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class ClientsRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientsRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Client> AddAsync(Client client)
        {
            Clients createClient = new()
            {
                Id = Guid.NewGuid(),
                Address = client.Address,
                Email = client.Email,
                Identifier = client.Identifier,
                Name = client.Name,
                Phone = client.Phone,
            };

            _context.Clients.Add(createClient);
            await _context.SaveChangesAsync();

            return new Client(client, createClient.Id);
        }

        public async Task UpdateAsync(Client client)
        {
            _context.Clients.Update(new()
            {
                Id = client.Id,
                Name = client.Name,
                Address = client.Address,
                Email = client.Email,
                Identifier = client.Identifier,
                Phone = client.Phone,
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Client client)
        {
            var clientToDelete = _context.Clients.FirstOrDefault(c => c.Id == client.Id);

            if (clientToDelete is not null)
            {
                _context.Clients.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}