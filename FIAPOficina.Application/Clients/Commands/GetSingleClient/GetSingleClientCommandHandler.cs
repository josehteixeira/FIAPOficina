using FIAPOficina.Domain.Clients.Entities;
using FIAPOficina.Domain.Clients.Repositories;

namespace FIAPOficina.Application.Clients.Commands.GetSingleClient
{
    internal class GetSingleClientCommandHandler
    {
        private readonly IClientRepository _repository;

        public GetSingleClientCommandHandler(IClientRepository repository)
        {
            _repository = repository;
        }

        public async Task<Client?> Handle(GetSingleClientCommand command)
        {
            if (string.IsNullOrEmpty(command.Identifier))
            {
                return await _repository.FirstOrDefaultAsync(command.Id).ConfigureAwait(false);
            }
            return await _repository.FirstOrDefaultAsync(command.Identifier).ConfigureAwait(false);
        }
    }
}