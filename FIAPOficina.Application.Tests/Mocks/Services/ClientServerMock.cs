using FIAPOficina.Application.Clients.Commands.CreateClient;
using FIAPOficina.Application.Clients.Commands.DeleteClient;
using FIAPOficina.Application.Clients.Commands.GetAllClients;
using FIAPOficina.Application.Clients.Commands.GetSingleClient;
using FIAPOficina.Application.Clients.Commands.UpdateClient;
using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Domain.Clients.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Mocks.Services
{
    internal class ClientServerMock : IClientsService
    {
        public Task<Client> AddAsync(CreateClientCommand command)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(DeleteClientCommand command)
        {
            throw new NotImplementedException();
        }

        public Client[] GetAll(GetAllClientsCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<Client?> GetSingleAsync(GetSingleClientCommand command)
        {
            return new Client("Emanuel Fontes", "96202913010", "47 999828521", "teste@teste.com", "Rua das couves", command.Id);
        }

        public Task<Client> UpdateAsync(UpdateClientCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
