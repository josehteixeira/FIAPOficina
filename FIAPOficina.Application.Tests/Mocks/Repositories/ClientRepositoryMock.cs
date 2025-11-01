using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;
using FIAPOficina.Domain.Materials.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Repositories
{
    internal class ClientRepositoryMock : IClientRepository
    {
        private readonly List<Client> _clients = new List<Client>();
        public async Task<Client> AddAsync(Client client)
        {
            client.Id = Guid.NewGuid();
            _clients.Add(client);
            return client;
        }

        public Task DeleteAsync(Guid id)
        {
            var client = _clients.FirstOrDefault(m => m.Id == id);
            if (client != null)
                _clients.Remove(client);

            return Task.CompletedTask;
        }

        public Task<Client?> FirstOrDefaultAsync(Guid id)
        {
            return Task.FromResult(_clients.FirstOrDefault(m => m.Id == id));
        }

        public Task<Client?> FirstOrDefaultAsync(string identifider)
        {
            return Task.FromResult(_clients.FirstOrDefault(m => m.Identifier == identifider));
        }

        public Client[] GetAll()
        {
            return _clients.ToArray();
        }

        public async Task UpdateAsync(Client client)
        {
            var clientDb = FirstOrDefaultAsync(client.Id).GetAwaiter().GetResult();
            if (clientDb is not null)
            {
                clientDb.Identifier =  client.Identifier;
                clientDb.Address = client.Address;
                clientDb.Phone =  client.Phone;
                clientDb.Email = client.Email;
                client.Name = client.Name;  
            }
        }
    }
}
