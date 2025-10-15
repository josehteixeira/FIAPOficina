using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;

namespace FIAPOficina.Infrastructure.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
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
            var clientToUpdate = _context.Clients.FirstOrDefault(c => c.Id == client.Id);

            if (clientToUpdate is not null)
            {
                clientToUpdate.Name = client.Name;
                clientToUpdate.Address = client.Address;
                clientToUpdate.Email = client.Email;
                clientToUpdate.Identifier = client.Identifier; 
                clientToUpdate.Phone = client.Phone;

                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientToDelete = _context.Clients.FirstOrDefault(c => c.Id == id);

            if (clientToDelete is not null)
            {
                _context.Clients.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync();
        }
    }
}