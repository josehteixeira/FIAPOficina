using FIAPOficina.Application.Clients.Commands.CreateClient;
using FIAPOficina.Application.Clients.Commands.DeleteClient;
using FIAPOficina.Application.Clients.Commands.GetAllClients;
using FIAPOficina.Application.Clients.Commands.GetSingleClient;
using FIAPOficina.Application.Clients.Commands.UpdateClient;
using FIAPOficina.Domain.Clients.Entities;

namespace FIAPOficina.Application.Clients.Services
{
    public interface IClientsService
    {
        public Task<Client> AddAsync(CreateClientCommand command);
        public Task<Client> UpdateAsync(UpdateClientCommand command);
        public Task DeleteAsync(DeleteClientCommand command);
        public Task<Client?> GetSingleAsync(GetSingleClientCommand command);
        public Client[] GetAll(GetAllClientsCommand command);
    }
}