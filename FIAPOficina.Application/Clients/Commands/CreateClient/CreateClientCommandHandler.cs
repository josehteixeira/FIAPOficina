using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Commands.CreateClient
{
    internal class CreateClientCommandHandler
    {
        private readonly IClientRepository _repository;

        public CreateClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Client> Handle(CreateClientCommand command)
        {
            var client = await _repository.AddAsync(new(
                    name: command.Name,
                    identifier: command.Identifier,
                    phone: command.Phone,
                    email: command.Email,
                    address: command.Address
                )
            ).ConfigureAwait(false);

            return client;
        }
    }
}