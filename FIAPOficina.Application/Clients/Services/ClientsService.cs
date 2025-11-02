using FIAPOficina.Application.Clients.Commands.CreateClient;
using FIAPOficina.Application.Clients.Commands.DeleteClient;
using FIAPOficina.Application.Clients.Commands.GetAllClients;
using FIAPOficina.Application.Clients.Commands.GetSingleClient;
using FIAPOficina.Application.Clients.Commands.UpdateClient;
using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Services
{
    public class ClientsService : IClientsService
    {
        private readonly CreateClientCommandHandler _createHandler;
        private readonly UpdateClientCommandHandler _updateHandler;
        private readonly DeleteClientCommandHandler _deleteHandler;
        private readonly GetSingleClientCommandHandler _querySingleHandler;
        private readonly GetAllClientsCommandHandler _queryAllHandler;

        public ClientsService(IClientRepository repository)
        {
            _createHandler = new CreateClientCommandHandler(repository);
            _updateHandler = new UpdateClientCommandHandler(repository);
            _deleteHandler = new DeleteClientCommandHandler(repository);
            _querySingleHandler = new GetSingleClientCommandHandler(repository);
            _queryAllHandler = new GetAllClientsCommandHandler(repository);
        }

        public async Task<Client> AddAsync(CreateClientCommand command)
        {
            return await _createHandler.Handle(command);
        }

        public async Task<Client> UpdateAsync(UpdateClientCommand command)
        {
            return await _updateHandler.Handle(command);
        }

        public async Task DeleteAsync(DeleteClientCommand command)
        {
            await _deleteHandler.Handle(command);
        }

        public async Task<Client?> GetSingleAsync(GetSingleClientCommand command)
        {
            return await _querySingleHandler.Handle(command).ConfigureAwait(false);
        }

        public Client[] GetAll(GetAllClientsCommand command)
        {
            return _queryAllHandler.Handle(command);
        }
    }
}