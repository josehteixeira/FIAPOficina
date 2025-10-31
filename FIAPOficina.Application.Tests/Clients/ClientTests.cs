using FIAPOficina.Application.Clients.Commands.CreateClient;
using FIAPOficina.Application.Clients.Commands.GetSingleClient;
using FIAPOficina.Application.Clients.Commands.DeleteClient;
using FIAPOficina.Application.Clients.Commands.GetAllClients;
using FIAPOficina.Application.Clients.Commands.UpdateClient;
using FIAPOficina.Application.Clients.Services;
using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Application.Tests.Mocks;
using FIAPOficina.Application.Common.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FIAPOficina.Application.Tests.Clients
{
    public class ClientTests
    {
        private readonly ClientRepositoryMock _mock;
        
        public ClientTests()
        {
            _mock = new ClientRepositoryMock();
        }

        [Fact]
        public void Should_Create_Client_Valid()
        {
            ClientsService clientsService = new ClientsService(_mock, null);

            var client = clientsService.AddAsync(new CreateClientCommand("Emanuel Fontes", "96202913010", "47 999828521", "teste@teste.com", "Rua das couves")).GetAwaiter().GetResult();

            Assert.NotNull(client);
            Assert.Equal("Emanuel Fontes", client.Name);
            Assert.Equal("96202913010", client.Identifier);
            Assert.Equal("47 999828521", client.Phone);
            Assert.Equal("teste@teste.com", client.Email);
            Assert.Equal("Rua das couves", client.Address);

            var clientDb = clientsService.GetSingleAsync(new GetSingleClientCommand(client.Id)).GetAwaiter().GetResult();

            Assert.NotNull(clientDb);
            Assert.Equal("Emanuel Fontes", clientDb.Name);
            Assert.Equal("96202913010", clientDb.Identifier);
            Assert.Equal("47 999828521", clientDb.Phone);
            Assert.Equal("teste@teste.com", clientDb.Email);
            Assert.Equal("Rua das couves", clientDb.Address);
        }

        [Fact]
        public void Should_Update_Client_Valid()
        {


            ClientsService clientsService = new ClientsService(_mock, null);
            var clientsDb = clientsService.GetAll(new GetAllClientsCommand());
            Client client;
            if (clientsDb is not null && clientsDb.Any())
                client = clientsDb[0];
            else
                client = clientsService.AddAsync(new CreateClientCommand("Emanuel Fontes", "96202913010", "47 999828521", "teste@teste.com", "Rua das couves")).GetAwaiter().GetResult();

            var clientUpdated = clientsService.UpdateAsync(new UpdateClientCommand(client.Id, "Emanuel Fontes", "96202913010", "47 999823521", "teste@teste.com", "Rua das saladas")).GetAwaiter().GetResult();

            Assert.NotNull(clientUpdated);
            Assert.Equal("Emanuel Fontes", clientUpdated.Name);
            Assert.Equal("96202913010", clientUpdated.Identifier);
            Assert.Equal("47 999823521", clientUpdated.Phone);
            Assert.Equal("teste@teste.com", clientUpdated.Email);
            Assert.Equal("Rua das saladas", clientUpdated.Address);
        }

        [Fact]
        public void Should_Delete_All_Client()
        {
            ClientsService clientsService = new ClientsService(_mock, null);
            var clientsDb = clientsService.GetAll(new GetAllClientsCommand());

            foreach (var client in clientsDb)
                clientsService.DeleteAsync(new DeleteClientCommand(client.Id)).GetAwaiter();

            clientsDb = null;
            clientsDb = clientsService.GetAll(new GetAllClientsCommand());

            Assert.Empty(clientsDb);
        }
    }
}
