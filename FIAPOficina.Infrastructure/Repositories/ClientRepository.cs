using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;
using FIAPOficina.Infrastructure.Database.Context;
using FIAPOficina.Infrastructure.Database.Entities;
using Microsoft.EntityFrameworkCore;

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
                Id = client.Id == Guid.Empty ? Guid.NewGuid() : client.Id,
                Address = client.Address,
                Email = client.Email,
                Identifier = client.Identifier,
                Name = client.Name,
                Phone = client.Phone,
            };

            _context.Clients.Add(createClient);
            await _context.SaveChangesAsync().ConfigureAwait(false);

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

                await _context.SaveChangesAsync().ConfigureAwait(false);
            }
        }

        public async Task DeleteAsync(Guid id)
        {
            var clientToDelete = _context.Clients.FirstOrDefault(c => c.Id == id);

            if (clientToDelete is not null)
            {
                _context.Clients.Remove(clientToDelete);
            }

            await _context.SaveChangesAsync().ConfigureAwait(false);
        }

        public async Task<Client?> FirstOrDefaultAsync(Guid id)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Id == id).ConfigureAwait(false);

            if (client is not null)
            {
                return new Client
                (
                    name: client.Name,
                    identifier: client.Identifier,
                    phone: client.Phone,
                    email: client.Email,
                    address: client.Address,
                    id: client.Id
                );
            }

            return null;
        }

        public async Task<Client?> FirstOrDefaultAsync(string identifider)
        {
            var client = await _context.Clients.FirstOrDefaultAsync(c => c.Identifier == identifider).ConfigureAwait(false);

            if (client is not null)
            {
                return new Client
                (
                    name: client.Name,
                    identifier: client.Identifier,
                    phone: client.Phone,
                    email: client.Email,
                    address: client.Address,
                    id: client.Id
                );
            }

            return null;
        }

        public Client[] GetAll()
        {
            var clients = _context.Clients.ToArray();

            return clients.Select(client =>
                new Client
                (
                    client.Name,
                    client.Identifier,
                    client.Phone,
                    client.Email,
                    client.Address,
                    client.Id
                )).ToArray();
        }
    }
}