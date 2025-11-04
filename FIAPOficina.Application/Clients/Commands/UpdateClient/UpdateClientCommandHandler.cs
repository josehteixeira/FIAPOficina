using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Commands.UpdateClient
{
    internal class UpdateClientCommandHandler
    {
        private readonly IClientRepository _repository;

        public UpdateClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Client> Handle(UpdateClientCommand command)
        {
            var client = new Client(
                name: command.Name,
                identifier: command.Identifier,
                phone: command.Phone,
                email: command.Email,
                address: command.Address,
                id: command.Id
            );

            await _repository.UpdateAsync(client).ConfigureAwait(false);

            return client;
        }
    }
}